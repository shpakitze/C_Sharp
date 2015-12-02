using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mordor;
using gondor;
using rohan;
namespace homework_3_z5
{
    class Program
    {
        static void Main(string[] args)
        {
            mordor.Barad_dur barad_dur = new Barad_dur(1000);
            gondor.Minas_Tirith minas_tirith = new Minas_Tirith(100);
            rohan.Edoras edoras = new Edoras(1001);
            int[] count={barad_dur.popul,minas_tirith.popul,edoras.popul};
            Array.Sort(count);
            foreach (int a in count)
                Console.WriteLine(a);
        }
    }
}
