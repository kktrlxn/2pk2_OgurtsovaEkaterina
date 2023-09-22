namespace PZ_05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double x = 0, b = 2.3, a = 1.45, h = 0.1;//объявление переменных
            while (x <= 1) { double y = x + b * x * Math.Sin(a);//цикл будет выполняться до тeх пор пока x<=1
                Console.WriteLine($"х = {x} y = {y}"); //вывод результата
                x += h; }
            Console.ReadLine();
        }
    }
}