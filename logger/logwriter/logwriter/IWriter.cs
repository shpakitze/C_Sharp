using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logwriter
{
    interface IWriter
    {
        void WriteCriticalErr(string mess);
        void WriteErr(string mess);
        void WriteInfo(string mess);
        void PrintLog();
    }
}
