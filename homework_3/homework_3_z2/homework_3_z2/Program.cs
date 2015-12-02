using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//задача 2.
namespace homework_3_z2
{
    class Program
    {
        static void Main(string[] args)
        {
            index prob = new index();
            string st = "6,8";
            try
            {
                prob = index.parse(st);
            }
            catch (Exception e)
            { Console.WriteLine(e.Message); }
            finally
            {
                Console.WriteLine(prob.a.ToString() + " " + prob.b.ToString());
            }
            try
            {
                prob = index.parse("ff f");
            }
            catch (Exception e)
            { Console.WriteLine(e.Message); }
            finally
            {
                Console.WriteLine(prob.a.ToString() + " " + prob.b.ToString());
            }
        }
    }
    struct index
    {
        public int a, b;

        public static index parse(string inp)
        {
            char[] ch = { ' ', ',' };
            string[] ind = inp.Split(ch);
            index rez = new index();
            if (ind.Length == 2)
            {
                try
                {
                    int a = Int32.Parse(ind[0]);
                    int b = int.Parse(ind[1]);
                    rez.a = a;
                    rez.b = b;
                }
                catch (Exception)
                {
                    throw new FormatException("blabla");
                }
            }
            return rez;
        }
    }
}
