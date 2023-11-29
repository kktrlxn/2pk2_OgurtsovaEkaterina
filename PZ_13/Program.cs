namespace PZ_13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //С помощью рекурсии вычислите n-й член арифметической прогрессии, значения первого элемента = -10 и шаг d =-2
            Console.Write("Введите номер члена арифметической прогрессии: ");
            int n = Convert.ToInt32(Console.ReadLine());
            int result = ArithmProgression(n);
            Console.WriteLine($"{n} член прогрессии = " + result);
            //С помощью рекурсии вычислить n-й член геометрической прогрессии, значения первого элемента = 12 и знаменатель прогрессии = -2
            Console.Write("Введите номер члена геометрической прогрессии: ");
            int n_2 = Convert.ToInt32(Console.ReadLine());
            int result_2 = GeomProgression(n_2);
            Console.WriteLine($"{n_2} член прогрессии = " + result_2);
            //Даны два целых числа A и В (каждое в отдельной строке). Выведите все числа от A до B включительно, используя рекурсию, в порядке возрастания, если A < B, или в порядке убывания в противном случае.
            Console.Write("Введите число А: ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите число B: ");
            int b = Convert.ToInt32(Console.ReadLine());
            if (a < b)
            {
                Console.WriteLine("Вывод чисел в порядке возрастания: ");
                OutputNumbers(a, b);
            }
            else if (a > b)
            {
                Console.WriteLine("Вывод чисел в порядке убывания: ");
                OutputNum(a, b);
            }
            else Console.WriteLine("Число А = В");
            Console.WriteLine(a);
            //С помощью рекурсии Summ(int x) для введенного числа n вычислить сумму чисел от 1 до n
            Console.Write("Введите число n: ");
            int n_3 = Convert.ToInt32(Console.ReadLine());
            int result_4 = Summ(n_3);
            Console.WriteLine($"Сумма чисел от 1 до {n} = {result_4}");
        }
        static int ArithmProgression(int n)//арифметическая прогрессия
        {
            if (n == 1)
                return -10;
            else
                return ArithmProgression(n - 1) - 2;
        }
        static int GeomProgression(int n)//геометрическая прогрессия
        {
            if (n == 1)
                return 12;
            else
                return GeomProgression(n - 1) * (-2);
        }
        static void OutputNumbers(int A, int B)//метод для вывода чисел в порядке возрастания
        {
            if (A <= B)
            {
                Console.WriteLine(A);
                A++;
                OutputNumbers(A, B);
            }
        }
        static void OutputNum(int A, int B)//метод для вывода чисел в порядке убывания
        {
            if (A >= B)
            {
                Console.WriteLine(A);
                A--;
                OutputNum(A, B);
            }
        }
        static int Summ(int n)//метод для вычисления суммы чисел от 1 до n
        {
            if (n == 1) return 1;
            else if (n < 1) return 0;
            else return n + Summ(n - 1);
        }
    }
}