using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logwriter
{
    public class LogExcp : ApplicationException
    {
        public LogExcp()
            : base()
        { }
        public LogExcp(string mess)
            : base(mess)
        { }
        public override string ToString()
        {
            return base.ToString();
        }
    }
    public class CantCreateLogFile : LogExcp
    {
        public CantCreateLogFile()
            : base()
        { }
        public CantCreateLogFile(string mess)
            : base(mess)
        { }
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class CantReadLogFile : LogExcp
    {
        public CantReadLogFile()
            : base()
        { }
        public CantReadLogFile(string mess)
            : base(mess)
        { }
        public override string ToString()
        {
            return base.ToString();
        }

    }

    public class CantWriteLogFile : LogExcp
    {
        public CantWriteLogFile()
            : base()
        { }
        public CantWriteLogFile(string mess)
            : base(mess)
        { }
        public override string ToString()
        {
            return base.ToString();
        }

    }
    public class SomeOtherCrashErr : LogExcp
    {
        public SomeOtherCrashErr()
            : base()
        { }
        public SomeOtherCrashErr(string mess)
            : base(mess)
        { }
        public override string ToString()
        {
            return base.ToString();
        }

    }
}
