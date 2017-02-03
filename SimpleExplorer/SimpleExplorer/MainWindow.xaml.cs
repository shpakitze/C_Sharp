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
using System.IO;
using System.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
namespace SimpleExplorer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitStartDir(LeftDirPanel);
            InitStartDir(RightDirPanel);
        }
        private void InitStartDir(ListBox lb)
        {
            DriveInfo[] alldrive = DriveInfo.GetDrives();

            lb.Items.Clear();
            lb.Items.Add("..");
            foreach (DriveInfo drive in alldrive)
            {
                lb.Items.Add(drive.ToString());

            }

            //LeftDirPanel.ItemsSource = alldrive;
        }

        private void DirPanel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox curLB = (ListBox)sender;
            Label curLabel;
            ListView curListView;
            if (curLB == LeftDirPanel) curLabel = LeftCurrentPath; else curLabel = RigthCurrentPath;
            if (curLB == LeftDirPanel) curListView = LeftFilePanel; else curListView = RightFilePanel;
            if (curLB.SelectedIndex == -1) { return; }
            if (((curLB.SelectedIndex == 0) && (curLabel.Content.ToString() == "")))
            { return; }
            if ((curLB.SelectedIndex == 0) && (curLabel.Content.ToString() != ""))
            {
                DirectoryInfo di = new DirectoryInfo(curLabel.Content.ToString());
                di = di.Parent;
                if (di == null)
                {
                    InitStartDir(curLB);
                    curLabel.Content = "";
                    curListView.Items.Clear();
                    return;
                }
                else
                {
                    curLabel.Content = di.FullName;
                }
            }
            else
            {
                curLabel.Content = System.IO.Path.Combine(curLabel.Content.ToString(), curLB.SelectedItem.ToString());
            }
            DirectoryInfo currentPath = new DirectoryInfo(curLabel.Content.ToString());
            //currentPath.Attributes = FileAttributes.Directory;
            DirectoryInfo[] alldir = currentPath.GetDirectories();
            curLB.Items.Clear();
            curLB.Items.Add("..");
            foreach (DirectoryInfo di in alldir)
            {
                curLB.Items.Add(di.Name);
            }
            curListView.Items.Clear();
            FileInfo[] fi = currentPath.GetFiles();
            foreach (FileInfo f in fi)
            {
                curListView.Items.Add(f.Name);
            }
            //curListView.ItemsSource = fi;
        }


        private void FilePanel_LostFocus(object sender, RoutedEventArgs e)
        {
            ((ListBox)sender).SelectedIndex = -1;

        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void LeftFilePanel_GotFocus(object sender, RoutedEventArgs e)
        {
            RightFilePanel.SelectedIndex = -1;
        }

        private void RightFilePanel_GotFocus(object sender, RoutedEventArgs e)
        {
            LeftFilePanel.SelectedIndex = -1;
        }

        private void FileCopy(string soursePath,string destPath)
        {
            //FileInfo currentFile = new FileInfo(soursePath,destPath);

        
        }
        private void LoadHexForm(string file)
        {
            ByteViewer bv = new ByteViewer();
            bv.SetFile(file);
            HexWinForm hx = new HexWinForm();
            hx.Controls.Add(bv);
            hx.Show();
        }

        private void HexButton_Click(object sender, RoutedEventArgs e)
        {
            LoadHexForm(System.IO.Path.Combine(LeftCurrentPath.Content.ToString(),LeftFilePanel.SelectedValue.ToString()));
        }

    }
}