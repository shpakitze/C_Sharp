using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
namespace bank_map
{
    public partial class MainForm : Form
    {
        public DataBaseBank.DataBaseBuild db;
        public MainForm()
        {
            InitializeComponent();
            db = new DataBaseBank.DataBaseBuild();
            MapForm Map = new MapForm(db.GetContext());
            Map.MdiParent = this;
            Map.Show();
            
            
        }

        private void fillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parser.Parser pars= new parser.Parser();
            pars.FillDB(db.GetContext());
        }

        private void getNewMoneyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parser.Parser pars = new parser.Parser();
            pars.GetNewMoneyData();
        }
    }
}
