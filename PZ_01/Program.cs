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
            Console.Write("Введите число a(пи=pi): ");
            string aInput = Console.ReadLine();
            Console.Write("Введите число b(пи=pi): ");
            string bInput = Console.ReadLine();
            Console.Write("Введите число c(пи=pi): ");
            string cInput = Console.ReadLine(); //пользователь вводит три числа

            double a, b, c; //объявление переменных
            //проверка условия для ввода числа пи
            if (aInput.ToLower() == "pi")
            {
                Console.Write("Введите знаменатель числа pi(при его отсутствии писать 1): ");
                double x = Convert.ToDouble(Console.ReadLine());
                a = Math.PI / x;
            }
            else { a = double.Parse(aInput); }
            if (bInput.ToLower() == "pi")
            {
                Console.Write("Введите знаменатель числа pi(при его отсутствии писать 1): ");
                double z = Convert.ToDouble(Console.ReadLine());
                b = Math.PI / z;
            }
            else { b = double.Parse(bInput); }
            if (cInput.ToLower() == "pi")
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
