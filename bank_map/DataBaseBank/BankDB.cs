using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataBaseBank
{

    [Table("Banks")]
    public class BankInfo
    {
        public BankInfo()
        {
            Deps = new List<DepInfo>();
            Money = new List<MoneyInfo>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int bankId { set; get; }
        public string name { set; get; }
        public virtual List<DepInfo> Deps { set; get; }
        public virtual List<MoneyInfo> Money { set; get; }
    }
    [Table("Departmens")]
    public class DepInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [ForeignKey("BankInfo")]
        public int BankInfoId { set; get; }
        public string address { set; get; }
        public string workTime { set; get; }
        public string description { set; get; }
        public double posX { set; get; }
        public double posY { set; get; }
        public BankInfo BankInfo { set; get; }
    }
    [Table("Kurs")]
    public class MoneyInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { set; get; }
        [ForeignKey("BankInfo")]
        public int BankInfoId { set; get; }
        public string date { set; get; }
        public double rybS { set; get; }
        public double rybB { set; get; }
        public double eurS { set; get; }
        public double eurB { set; get; }
        public double usdS { set; get; }
        public double usdB { set; get; }
       public BankInfo BankInfo { set; get; }
    }
}
