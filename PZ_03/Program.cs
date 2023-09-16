namespace PZ_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите ваш возраст: "); //ввод возраста
            int age = Convert.ToInt32(Console.ReadLine());
            int x = age % 10; //проверка остатка от 10
            int y = age % 100; //проверка остатка от 100
            switch (x) //проверка остатка для определения верного склонения
            {
                case 1:
                    if (y != 11) { Console.WriteLine($"Вам {age} год."); }
                    else { Console.WriteLine($"Вам {age} лет."); }
                    break;
                case 2:
                case 3:
                case 4:
                    if (y != 12 && y != 13 && y != 14) { Console.WriteLine($"Вам {age} года."); }
                    else { Console.WriteLine($"Вам {age} лет."); }
                    break;
                    default: Console.WriteLine($"Вам {age} лет.");
                    break;
            }
        }
    }
}