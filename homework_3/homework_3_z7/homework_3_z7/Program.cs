using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_3_z7
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Fraction f = new Fraction(3, 4);
            int a = 10;
            Fraction f1 = f * a;
            Console.WriteLine(f1.ToString());
            Fraction f2 = a * f;
            Console.WriteLine(f2.ToString());
            double d = 1.5;
            Fraction f3 = f + d;
            Console.WriteLine(f3.ToString());
        }
    }
    class Fraction
    {
        private int _numerator, _denominator;
        public Fraction()
        {
            numerator = 0;
            denominator = 1;
        }
        public Fraction(int n, int k)
        {
            numerator = n;
            denominator = k;
        }
        public int denominator
        {
            set { if ((int)value != 0) _denominator = value; }
            get { return _denominator; }
        }
        public int numerator
        {
            set { _numerator = value; }
            get { return _numerator; }
        }
        public static Fraction operator *(Fraction a, Fraction b)
        {
            Fraction rez = new Fraction(a.numerator * b.numerator, a.denominator * b.denominator);
            rez.simplify();
            return rez;
        }
        public static Fraction operator *(Fraction a, int b)
        {
            Fraction rez = new Fraction(a.numerator * b, a.denominator);
            rez.simplify();
            return rez;
        }
        public static Fraction operator *(int a, Fraction b)
        {
            Fraction rez = new Fraction();
            rez = b * a;
            rez.simplify();
            return rez;
        }
        public static Fraction operator /(Fraction a, Fraction b)
        {
            Fraction rez = new Fraction(a.numerator * b.denominator, a.denominator * b.numerator);
            rez.simplify();
            return rez;
        }
        public static Fraction operator +(Fraction a, Fraction b)
        {
            Fraction rez = new Fraction(b.denominator * a.numerator + a.denominator * b.numerator, a.denominator * b.denominator);
            rez.simplify();
            return rez;
        }
        public static Fraction operator +(Fraction a, int b)
        {
            Fraction rez = new Fraction();
            rez.denominator = a.denominator;
            rez.numerator = a.numerator + b * a.denominator;
            rez.simplify();
            return rez;
        }
        public static Fraction operator +(Fraction a, double b)
        {
            Fraction rez = new Fraction();
            Fraction Fb;
            Fb = (Fraction)b;
            rez = a + Fb;
            rez.simplify();
            return rez;
        }
        public static explicit operator Fraction(double b)
        {
            Fraction rez = new Fraction();
            int intDigit = (int)Math.Truncate(b);
            double FractionalDigit = Math.Abs(b - intDigit);
            int countZerro = FractionalDigit.ToString().Length - 2;
            rez.denominator = (int)Math.Pow(10, countZerro);

            rez.numerator = (int)(b * rez.denominator);
            rez.simplify();
            return rez;
        }
        public static explicit operator Fraction(int b)
        {
            Fraction rez = new Fraction();
            rez.numerator = b;
            rez.denominator = 1;
            return rez;
        }
        public static Fraction operator -(Fraction a)
        {
            return new Fraction(-a.numerator, a.denominator);
        }
        public static Fraction operator -(Fraction a, Fraction b)
        {
            Fraction rez = new Fraction();
            rez = a + (-b);
            rez.simplify();
            return rez;
        }
        public override string ToString()
        {
            if (denominator != 1)
                return numerator.ToString() + "/" + denominator.ToString();
            else return numerator.ToString();
        }
        private int NOD()
        {
            int a = Math.Abs(numerator);
            int b = denominator;
            if ((a != 0) && (b != 0))
            {
                while (a != b)
                {
                    if (a > b)
                        a = a - b;
                    else
                        b = b - a;
                }
                return a;
            }
            else return 1;
        }
        public void simplify()
        {
            int nod = this.NOD();
            this.numerator = this.numerator / nod;
            this.denominator = this.denominator / nod;
        }
        public static bool operator >(Fraction a, Fraction b)
        {
            if ((a - b).numerator > 0) return true; else return false;
        }
        public static bool operator <(Fraction a, Fraction b)
        {
            if ((a - b).numerator < 0) return true; else return false;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Fraction a = obj as Fraction;
            if (a == null) return false;
            this.simplify();
            a.simplify();
            return ((this.numerator == a.numerator) && (this.denominator == a.denominator));
        }
        public static bool operator ==(Fraction a, Fraction b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null) return false;
            return (a.Equals(b));

        }
        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);
        }
        public override int GetHashCode()
        {
            return numerator ^ denominator;
        }
        public static bool operator true (Fraction a)
        {
            if (Math.Abs(a.numerator) > a.denominator) return true;
            else
                return false;
        }
        public static bool operator false(Fraction a)
        {
            if (Math.Abs(a.numerator) < a.denominator) return true;
            else
                return false;
        }
    }
}

