using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using DataBaseBank;
using System.Globalization;
namespace parser
{
    interface IParse
    {
        void FillDB(DataBaseBank.BankDBContext dbContext);
    }
    public class Parser : IParse
    {
        public void gparser()
        {

        }
        private List<string> GetUrl()
        {
            HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.GetEncoding(1251);
            HtmlDocument doc = web.Load(@"http://select.by/kurs");
            HtmlNodeCollection bank = doc.DocumentNode.SelectNodes("//table[@id='curr_table']/tbody/tr");
            List<string> depUrl = new List<string>();
            foreach (var item in bank)
            {
                if (item.HasAttributes)
                {
                    var a = item.SelectNodes(".//td/a").First().GetAttributeValue("href", "");
                    depUrl.Add(a);
                }
            }
            return depUrl;
        }
        public void GetNewMoneyData()
        {
            HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.GetEncoding(1251);
            HtmlDocument doc = web.Load(@"http://select.by/kurs");
            HtmlNodeCollection bank = doc.DocumentNode.SelectNodes("//table[@id='curr_table']/tbody/tr");
            var money = new DataBaseBuild();
            
            string currBankName = "";
            foreach (var item in bank)
            {
                if ((!item.HasAttributes))
                {
                    if ((item.SelectNodes(".//td").First().HasAttributes))
                    {
                        var b = item.SelectNodes("./td").Skip(1).ToList();


                        MoneyInfo newMoney = new MoneyInfo();
                        currBankName = b[0].InnerText;
                        NumberFormatInfo format = new NumberFormatInfo();
                        format.NumberDecimalSeparator = ",";
                        newMoney.usdB = double.Parse(b[1].InnerText, format);
                        newMoney.usdS = double.Parse(b[2].InnerText, format);
                        newMoney.eurB = double.Parse(b[3].InnerText, format);
                        newMoney.eurS = double.Parse(b[4].InnerText, format);
                        newMoney.rybB = double.Parse(b[5].InnerText, format);
                        newMoney.rybS = double.Parse(b[6].InnerText, format);
                        newMoney.BankInfoId = money.GetBankId(currBankName);
                        
                        newMoney.date = DateTime.Now.ToShortDateString();
                        money.AddMoneyInfo(newMoney);

                    }
                }

            }
        }
        public void FillDB(DataBaseBank.BankDBContext dbContext)
        {
            DataBaseBuild dbBuild = new DataBaseBuild();
            List<string> depUrl = GetUrl();
            HtmlWeb web = new HtmlWeb();
            foreach (var item in depUrl)
            {
                DepInfo newDep = new DepInfo();
                BankInfo newBank = new BankInfo();
                int curBankId = 0;
                // DataBaseBank.BankDBContext dbContext = dbBuild.GetContext();
                //HtmlWeb webBank = new HtmlWeb();
                web.OverrideEncoding = Encoding.GetEncoding(1251);
                HtmlDocument url = web.Load(item);

                //doc.LoadHtml(@"http://select.by/kurs/karta/id790");
                HtmlNodeCollection met = url.DocumentNode.SelectNodes("//meta");

                foreach (var data in met)
                {
                    if (data.GetAttributeValue("itemprop", "") == "latitude")
                        newDep.posX = double.Parse(data.GetAttributeValue("content", ""), CultureInfo.InvariantCulture);
                    if (data.GetAttributeValue("itemprop", "") == "longitude")
                        newDep.posY = double.Parse(data.GetAttributeValue("content", ""), CultureInfo.InvariantCulture);
                }
                HtmlNodeCollection bankInfo = url.DocumentNode.SelectNodes("//span");
                string tempName = "";
                foreach (var bankI in bankInfo)
                {

                    if (bankI.GetAttributeValue("itemprop", "") == "name")
                        tempName = bankI.InnerText;


                    if (bankI.GetAttributeValue("itemprop", "") == "streetAddress")
                        newDep.address = bankI.InnerText;


                }
                curBankId = dbContext.Banks.Count();
                var res = dbContext.Banks.Where(s => s.name == tempName).FirstOrDefault();
                if (res != null)
                {
                    newDep.BankInfoId = res.bankId;
                    //res.Deps.Add();
                }
                else
                {
                    newBank.name = tempName;
                    newBank.bankId = curBankId;

                    ++curBankId;
                    dbBuild.AddBank(newBank);
                    newDep.BankInfoId = newBank.bankId;
                }
                dbBuild.AddDep(newDep);
            }
        }
    }
}
