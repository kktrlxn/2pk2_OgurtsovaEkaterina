namespace PZ_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double b, c, e, d; //определение переменных
            Console.Write("Введите число b: "); //ввод действительного числа
            b = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите целое число a: "); //ввод целого числа
            int a = Convert.ToInt32(Console.ReadLine());
            if ( a < 0 ) //проверка условия у числа а
            {
                c = (a - (b * (Math.Pow(a, 3)) / b)); //вычисления
            }
            else { c = (a * b - 10); } //обратное условие
            if (c <= 5) //проверка условия числа c 
            {
                d = (Math.Sin(2 * a) + 2 * c); //вычисления
            }
            else { d = (Math.Pow(Math.Cos((a - b) / (c - a)), 2)); } //обратное условие

            e = ((2 * b + 3 * d - 11 * a * c) / 10); //вычисления
            Console.WriteLine(e); //вывод ответа
        }
    }
}
