using System;

namespace PZ_12
{
    internal class Program
    {
        //вычисление длины вектора
        static double VectorLenght(double x_1, double y_1, double x_2, double y_2)
        {
            return Math.Abs(Math.Sqrt(Math.Pow((x_2 - x_1), 2) + Math.Pow((y_2 - y_1), 2)));
        }
        //инвертация регистра
        static string InvertString(string text)
        {
            char[] array = text.ToCharArray();//создание массива символов из строки
            for (int i = 0; i < array.Length; i++)
            {
                if (char.IsUpper(array[i]))//если регистр заглавный, то он поменяется на нижний
                { array[i] = char.ToLower(array[i]); }
                else if (char.IsLower(array[i]))//если регистр нижний, то он поменяется на заглавный
                { array[i] = char.ToUpper(array[i]); }
            }
            return new string(array);
        }
        static void Main(string[] args)
        {
            //Вычисление длины вектора
            Console.Write("Введите координату х1 первой точки вектора: ");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите координату y1 первой точки вектора: ");
            double b = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите координату х2 второй точки вектора: ");
            double c = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите координату у2 второй точки вектора: ");
            double d = Convert.ToDouble(Console.ReadLine());
            double len = VectorLenght(a, b, c, d);
            Console.WriteLine("Длина вектора = " + len);
            //Реализуйте метод, принимающий произвольный текст и возвращающий этот же текст, где все символы инвертированы в регистре. Пример: “Simple String” -> “sIMPLE
            Console.Write("Введите строку с использованием разного регистра: ");
            string s = Console.ReadLine();
            string str = (InvertString(s));
            Console.WriteLine("Полученная строка: " + str);
        }
    }
}