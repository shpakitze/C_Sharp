//not used
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Threading;

namespace SimpleExplorer
{
    public delegate void ProgressChangeDelegate(double persente, ref bool cancel);
    public delegate void CompleteDelegate();
    class FileCopier
    {
        public string SourcePath { set; get; }
        public string DestPath { set; get; }
        FileCopyProg Fcp;
        double persent;
        private void ProgressChange(double persente, ref bool cancel)
        {
            
            Fcp.CopyProgress.Value = persente;
            
            
            cancel = false;
        }
        private void Complete()
        {
            MessageBox.Show("OK");
        }
        public FileCopier(string source, string dest)
        {
            OnProgressChanged+=ProgressChange;
            OnComplete += Complete;
            SourcePath = source;
            DestPath = dest;
            Fcp = new FileCopyProg(source,dest);
            Fcp.CopyProgress.Minimum = 0;
            Fcp.CopyProgress.Maximum = 100;
            
            Fcp.Show();
            Copy();

        }
        public void Copy()
        {
            byte[] bufer = new byte[1024*1024];
            bool cancelFlag = false;

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
                        persent = (double)totalBytes * 100.0 / FileLength;
                        dest.Write(bufer, 0, currentBlockSize);
                        cancelFlag = false;
                        OnProgressChanged(persent, ref cancelFlag);
                        if (cancelFlag)
                        {
                            //del dest file
                            break;
                        }

                    }
                }
            }
            OnComplete();
        }

        public event ProgressChangeDelegate OnProgressChanged;
        public event CompleteDelegate OnComplete;
    }


}
