namespace PZ_07
{
    internal class Program
    {
        static void Main(string[] args)
        {//Двумерный массив MxN заполнить случайными символами английского алфавита (заглавные).Вывести на экран количество повторений каждого символа.
            Console.Write("Введите число строк в массиве: ");//ввод строк и столбцов массива с консоли
            int M = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите число столбцов в массиве: ");
            int N = Convert.ToInt32(Console.ReadLine());

            char[,] array = new char[M, N];//объявление массива

            Random random = new Random();//рандом
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    array[i, j] = (char)('A' + random.Next(26));//заполнение массива
                    Console.Write(array[i, j] + " ");//вывод массива на консоль
                }
                Console.WriteLine();
            }
            int[] count = new int[26];//создание массива для подсчета повторений
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    char c = array[i, j];//подсчет
                    int x = c - 'A';
                    count[x]++;
                }
            }
            Console.WriteLine("Количество повторений каждого символа:");//вывод
            for (int i = 0; i < 26; i++)
            {
                Console.WriteLine((char)('A' + i) + ": " + count[i]);
            }
        }
    }
}