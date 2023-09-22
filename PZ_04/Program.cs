namespace PZ_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Первое задание");
            {//Вывести на экран построчно целые числа от 10 до 80 с указанным шагом = 4.
                int i;
                for (i = 10; i <= 80; i += 4) Console.WriteLine(i);
            }
            Console.WriteLine("Второе задание");
            {//Вывести на экран в одну строку 7 символов в алфавитном порядке, начиная с символа В (ru)
                char s = 'В';
                for (int x = 0; x < 7; x++)
                { char c = (char)(s + x); Console.Write(c); }
                Console.WriteLine();
            }
            Console.WriteLine("Третье задание");
            {//Вывести на экран посимвольно 7 знаков ‘#’ в 7 строках.
                int n, m;
                for (n = 0; n < 7; n++) { for (m = 0; m < 7; m++) { Console.Write("# "); } Console.WriteLine(); }
            }
            Console.WriteLine("Четвертое задание");
            {//Из диапазона от -500 до -200 вывести на экран значения, кратные 5, через пробел, используя один цикл. В конце вывести количество кратных чисел.
                int y, k = 0;
                for (y = -500; y <= -200; y++)
                { if (y % 5 == 0) Console.Write(y + " "); k++; }
                Console.WriteLine("\nКоличество чисел, кратных 5: " + k);

            }
            Console.WriteLine("Пятое задание");
            {//Выводить на экран значения 5 и 70, на каждом шаге итерации одну переменную инкрементировать, а вторую декрементировать до тех пор, пока разница между ними не будет равна (или меньше) 19
                int e, j;
                for (e = 5, j = 70; j - e > 19; e++, j--) Console.WriteLine($"{e} {j}");
                Console.WriteLine($"Разница между числами равна {j - e}");
            }
        }
    }
}
