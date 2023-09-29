namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
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

        }
    }
}