using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ_01
{
    class Program
    {
        static void Main(string[] args)
        {
            double a, b, c; //объявление переменных
            Console.Write("Введите число a(пи=pi): "); //ввод числа а
            string aInput = Console.ReadLine();
            if (aInput.ToLower() == "pi") //проверка условия для расчетов с числом пи
            {
                Console.Write("Введите знаменатель числа pi(при его отсутствии писать 1): ");
                double x = Convert.ToDouble(Console.ReadLine());
                a = Math.PI / x;
            }
            else { a = double.Parse(aInput); }

            Console.Write("Введите число b(пи=pi): "); //ввод числа b
            string bInput = Console.ReadLine();
            if (bInput.ToLower() == "pi") //проверка условия для расчетов с числом пи
            {
                Console.Write("Введите знаменатель числа pi(при его отсутствии писать 1): ");
                double z = Convert.ToDouble(Console.ReadLine());
                b = Math.PI / z;
            }
            else { b = double.Parse(bInput); }

            Console.Write("Введите число c(пи=pi): "); //ввод числа с
            string cInput = Console.ReadLine();
            if (cInput.ToLower() == "pi") //проверка условия для расчетов с числом пи
            {
                Console.Write("Введите знаменатель числа pi(при его отсутствии писать 1): ");
                double y = Convert.ToDouble(Console.ReadLine());
                c = Math.PI / y;
            }
            else { c = double.Parse(cInput); }
            
            double result1, result2, result; //объявление переменных
            result1 = Convert.ToDouble((Math.Log(Math.Pow(b, -(Math.Sqrt(Math.Abs(a)))))) * (a - b / 2));//выполнение первого действия
            result2 = Convert.ToDouble(Math.Pow(Math.Sin(c), 2) * Math.Atan(c));//выполнение второго действия
            result = result1 + result2;//выполнение третьего действия
            Console.WriteLine(result);//вывод результата
            Console.ReadKey();
        }
    }
}
