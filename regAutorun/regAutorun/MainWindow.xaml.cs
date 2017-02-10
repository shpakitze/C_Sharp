using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Collections.ObjectModel;
namespace regAutorun
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public class RegInfo
    {
        public string Name { set; get; }
        public string Value { set; get; }
        public RegInfo(string name, string val)
        {
            this.Name = name;
            Value = val;
        }
    }
    public partial class MainWindow : Window
    {
        public ObservableCollection<RegInfo> autoruns { set; get; }
        //List<RegInfo> autoruns;
        public MainWindow()
        {
            InitializeComponent();
            autoruns = new ObservableCollection<RegInfo>();
            GetAtorunProg();
            prog.ItemsSource = autoruns;

        }
        public void GetAtorunProg()
        {
            RegistryKey regKey;
            regKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            foreach (var item in regKey.GetValueNames())
            {

                autoruns.Add(new RegInfo(item, regKey.GetValue(item).ToString()));
            }
        }
        public void SetAtorunProg(string progName, string path)
        {
            RegistryKey regKey;
            regKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            regKey.SetValue(progName, path);
        }

        private void prog_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RegistryKey regKey;
            regKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            var a = (RegInfo)prog.SelectedItem;
            regKey.DeleteValue(a.Name);
            autoruns.Remove(a);
        }

        private void prog_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.ShowDialog();
            SetAtorunProg(f.SafeFileName, f.FileName);
            autoruns.Add(new RegInfo(f.SafeFileName, f.FileName));

        }
    }
}
