using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBaseBank;
using System.Xml;
namespace bank_map
{
    public partial class MapForm : Form
    {
        GMapControl gMapctr;
        DataBaseBank.BankDBContext bankDBContext;
        public MapForm()
        {
            InitializeComponent();
            Load += MapForm_Load;
        }
        public MapForm(DataBaseBank.BankDBContext val)
        {
            InitializeComponent();
            bankDBContext = val;
        }
        private void MapForm_Load(object sender, EventArgs e)
        {
            SetParamsMap();
        }
        public void exportXML(DataBaseBank.BankDBContext val)
        {
            XmlTextWriter writer = null;
            writer = new XmlTextWriter("banks.xml",System.Text.Encoding.Unicode);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("Banks");
            var res=val.Banks.ToList();
            foreach(var i in res)
            {
                writer.WriteStartElement("bank");
                writer.WriteAttributeString("name", i.name);
                foreach (var k in i.Deps)
                {
                    writer.WriteStartElement("departmen");
                    writer.WriteElementString("id",k.Id.ToString());
                    writer.WriteElementString("address", k.address);
                    writer.WriteElementString("latitude", k.posX.ToString());
                    writer.WriteElementString("longitude", k.posY.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Close();
        }
        void SetParamsMap()
        {
            gMapctr = new GMapControl();
            gMapctr.Dock = DockStyle.Fill;
            this.Controls.Add(gMapctr);
            gMapctr.MapProvider = GMap.NET.MapProviders.GMapProviders.OpenStreetMap;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gMapctr.MaxZoom = 18;
            gMapctr.MinZoom = 2;
            gMapctr.Zoom = 17;
            gMapctr.Position = new GMap.NET.PointLatLng(53.9028, 27.561729);
            gMapctr.MarkersEnabled = true;
            //GMap.NET.WindowsForms.GMapOverlay markersOverlay =
            //new GMap.NET.WindowsForms.GMapOverlay("marker");
            //var rez = bankDBContext.Banks.FirstOrDefault().Deps;

            bankDBContext.SaveChanges();
            var res = bankDBContext.Deps;// && Enumerable.Empty<DataBaseBank.DepInfo>();
            exportXML(bankDBContext);
            if (res != null)
            {
                foreach (var item in res)
                {
                    GMapOverlay markersOverlay = new GMapOverlay("NewMarkers");
                    //GMapMarkerGoogleGreen markerG = new GMapMarkerGoogleGreen
                    //                             (new GMap.NET.PointLatLng(item.posX,item.posY));
                    GMap.NET.WindowsForms.Markers.GMarkerGoogle markerG = new GMarkerGoogle(new GMap.NET.PointLatLng(item.posX, item.posY), GMarkerGoogleType.green);
                    markerG.ToolTip = new GMapRoundedToolTip(markerG);
                    DataBaseBank.BankDBContext temp = new DataBaseBank.DataBaseBuild().GetContext();
                    int bankid = temp.Banks.Where(x => x.bankId == item.BankInfoId).FirstOrDefault().bankId;
                    //DataBaseBuild dbmoney = new DataBaseBuild();
                    //MoneyStr curMoneyVal = dbmoney.GetKurs(item.BankInfoId);
                    //string result = item.address + "\n" + temp.Banks.Where(x => x.bankId == item.BankInfoId).FirstOrDefault().name + "\n"
                    //+ "eur" + curMoneyVal.eurB.ToString() + "  " + curMoneyVal.eurS.ToString()
                    //+ "usd" + curMoneyVal.usdB.ToString() + "  " + curMoneyVal.usdS.ToString()
                    //+ "rub" + curMoneyVal.rybB.ToString() + "  " + curMoneyVal.rybS.ToString();
                    //result+="\n"+temp.
                    
                    //markerG.ToolTipText = result;
                    markersOverlay.Markers.Add(markerG);
                    gMapctr.Overlays.Add(markersOverlay);

                }
            }
        }
    }
}
