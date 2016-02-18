using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace logwriter
{
    public class Logger : IWriter
    {
        private string fileName;
        private Logger()
        { }
        public Logger(string logfile)
        {
            fileName = logfile;
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.CreateNew);
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine("logfile create at {0}", DateTime.Now);
                }
            }
            catch (Exception e)
            {
                throw new CantCreateLogFile(e.Message);
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }
            //File 
        }
        private void WriteSomething(string caption, string mess)
        {
            FileStream fs = null;
            
            try
            {
                fs = new FileStream(fileName, FileMode.Append,FileAccess.Write);
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine("{0} : {1} : {2}",caption, DateTime.Now, mess);
                }
            }
            catch (Exception e)
            {
                throw new CantWriteLogFile(e.Message);
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }
        }
        public void WriteCriticalErr(string mess)
        {
            WriteSomething("Crit error!!!   ",mess);
        }
        public void WriteErr(string mess)
        {
            WriteSomething("Error           ", mess);
        }
        public void WriteInfo(string mess)
        {
            WriteSomething("Info            ", mess);
        }
        public void PrintLog()
        {
            try
            {
                StreamReader LogF = new StreamReader(fileName);
                while (!LogF.EndOfStream)
                {
                    Console.WriteLine(LogF.ReadLine());
                }
                LogF.Close();
            }
            catch (Exception e)
            {
                throw new CantReadLogFile(e.Message);
            }
        }
    }
}
