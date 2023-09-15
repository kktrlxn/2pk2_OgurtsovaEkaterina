namespace PZ_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double b, c, e, d;
            Console.Write("Введите число b: ");
            b = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите целое число a: ");
            int a = Convert.ToInt32(Console.ReadLine());
            if ( a < 0 )
            {
                c = (a - (b * (Math.Pow(a, 3)) / b));
            }
            else { c = (a * b - 10); }
            if (c <= 5)
            {
                d = (Math.Sin(2 * a) + 2 * c);
            }
            else { d = (Math.Pow(Math.Cos((a - b) / (c - a)), 2)); }

            e = ((2 * b + 3 * d - 11 * a * c) / 10);
            Console.WriteLine(e);
        }
    }
}