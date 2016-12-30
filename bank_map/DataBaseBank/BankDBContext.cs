using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataBaseBank
{
    public class BankDBContext:DbContext
    {
        public BankDBContext()
            : base(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\admin\Documents\Visual Studio 2013\Projects\bank_map\BankDB.mdf;Integrated Security=True;Connect Timeout=30"){}
        public DbSet<BankInfo> Banks {set; get;}
        public DbSet<DepInfo> Deps {set; get;}
        public DbSet<MoneyInfo> Money { set; get; }
    }
}
