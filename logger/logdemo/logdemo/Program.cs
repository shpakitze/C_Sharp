using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using logwriter;
namespace logdemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger Logger1 = null;
            try
            {
                Logger1 = new logwriter.Logger("C:\\test\\1.log");

                Logger1.WriteCriticalErr("Crit error blabla");
                Logger1.WriteCriticalErr("Crit error blabla");
                Logger1.WriteCriticalErr("Crit error blabla");
                Logger1.WriteCriticalErr("Crit error blabla");
                Logger1.WriteCriticalErr("Crit error blabla");
                Logger1.WriteErr("errrrrrrror");
                Logger1.WriteInfo("some info");
            }
            catch (logwriter.LogExcp e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                Logger1.PrintLog();
            }
            catch (logwriter.LogExcp e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
