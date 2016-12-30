using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataBaseBank
{
    public struct MoneyStr
    {
        public double usdB;
        public double usdS;
        public double eurB;
        public double eurS;
        public double rybB;
        public double rybS;
    }

    public interface IBaseOpeartion
    {
        void AddBank(BankInfo data);
        void AddDep(DepInfo data);
        void AddMoneyInfo(MoneyInfo data);
        BankDBContext GetContext();
        int GetBankId(string val);
        MoneyStr GetKurs(int id);
    }
    public class DataBaseBuild : IBaseOpeartion
    {
         
        public void AddBank(BankInfo data)
        {
            using (var db = new BankDBContext())
            {
                db.Banks.Add(data);
                db.SaveChanges();
            }
        }
        public void AddDep(DepInfo data)
        {
            using (var db = new BankDBContext())
            {
                db.Deps.Add(data);
                db.SaveChanges();
            }
        }
        public BankDBContext GetContext()
        {
            return new BankDBContext();
        }
        public void AddMoneyInfo(MoneyInfo data)
        {
            using (var db = new BankDBContext())
            {
                db.Money.Add(data);
                db.SaveChanges();
            }
        }


        public int GetBankId(string val)
        {
            using (var db = new BankDBContext())
            {

                return db.Banks.Where(x => x.name == val).First().bankId;
            }
        }


        public MoneyStr GetKurs(int id)
        {
            using (var db = new BankDBContext())
            {
                var res = db.Money.Where(x => x.BankInfoId == id).First();
                MoneyStr resultMoney;
                resultMoney.rybB = res.rybB;
                resultMoney.rybS = res.rybS;
                resultMoney.usdB = res.usdB;
                resultMoney.usdS = res.usdS;
                resultMoney.eurB = res.eurB;
                resultMoney.eurS = res.eurS;
                return resultMoney;
                
            } 
        }
    }
}
