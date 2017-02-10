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
using System.Windows.Shapes;
using System.ComponentModel;
using System.IO;
namespace SimpleExplorer
{
    /// <summary>
    /// Логика взаимодействия для FileCopyProg.xaml
    /// </summary>
    public partial class FileCopyProg : Window
    {
        private string SourcePath { set; get; }
        private string DestPath { set; get; }
        private BackgroundWorker bw = new BackgroundWorker();
        public FileCopyProg(string source, string dest)
        {
            SourcePath = source;
            DestPath = dest;
            InitializeComponent();
            this.CopyProgress.Minimum = 0;
            this.CopyProgress.Maximum = 100;
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_runWorkerCompleted);
            bw.RunWorkerAsync();
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] bufer = new byte[1024 * 1024];
            bool cancelFlag = false;
            BackgroundWorker worker = sender as BackgroundWorker;
            using (FileStream source = new FileStream(SourcePath, FileMode.Open, FileAccess.Read))
            {
                long FileLength = source.Length;
                using (FileStream dest = new FileStream(DestPath, FileMode.CreateNew, FileAccess.Write))
                {
                    long totalBytes = 0;
                    int currentBlockSize = 0;
                    while ((currentBlockSize = source.Read(bufer, 0, bufer.Length)) > 0)
                    {
                        totalBytes += currentBlockSize;
                        int persent = (int)(totalBytes * 100 / FileLength);
                        dest.Write(bufer, 0, currentBlockSize);
                        System.Threading.Thread.Sleep(500);
                        cancelFlag = false;
                        worker.ReportProgress(persent);
                        if (cancelFlag)
                        {
                            //del dest file
                            break;
                        }

                    }
                }
            }

            



        }
        private void bw_runWorkerCompleted(object sender,RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done");
            this.Close();
        }
        private void bw_ProgressChanged(object sender,ProgressChangedEventArgs e)
        {
            this.CopyProgress.Value = e.ProgressPercentage;
        }
    }
}
