namespace PZ_11
{
    internal class Program
    {
        static void Swap(ref double x, ref double y)//метод для замены мест переменных
        {
            double temp = x;
            x = y;
            y = temp;
        }
        static void Main(string[] args)
        {
            double a, b, c, d;
            Console.Write("Введите число а: ");//ввод чисел
            a = double.Parse(Console.ReadLine());
            Console.Write("Введите число b: ");
            b = double.Parse(Console.ReadLine());
            Console.Write("Введите число c: ");
            c = double.Parse(Console.ReadLine());
            Console.Write("Введите число d: ");
            d = double.Parse(Console.ReadLine());
            Swap(ref a, ref b);//вызов функции
            Swap(ref c, ref d);
            Swap(ref b, ref c);
            Console.WriteLine("Число а равно введенному числу b: " + a);//вывод функции
            Console.WriteLine("Число b равно введенному числу d: " + b);
            Console.WriteLine("Число c равно введенному числу a: " + c);
            Console.WriteLine("Число d равно введенному числу с: " + d);
        }
    }
}