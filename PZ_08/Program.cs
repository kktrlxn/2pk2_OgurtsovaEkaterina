namespace PZ_08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вывод ступенчатого массива: ");
            int[][] array = new int[10][];
            Random rnd = new Random();//рандом

            for (int i = 0; i < 10; i++)
            {
                int n = rnd.Next(5, 36);
                array[i] = new int[n];

                for (int j = 0; j < n; j++)
                {
                    array[i][j] = rnd.Next(1, 100);
                    Console.Write(array[i][j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nВывод одномерного массива с последними значениями строки ступенчатого массива: ");
            int[] arr = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                arr[i] = array[i][array[i].Length - 1];
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();

            Console.WriteLine("\nВывод одномерного массива с максимальными значениями строки ступенчатого массива: ");
            int[] max = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                int m = int.MinValue;
                foreach (int j in array[i])
                { if (j > m) { m = j; } }
                max[i] = m;
                Console.Write(max[i] + " ");
            }
            Console.WriteLine();

            Console.WriteLine("\nВывод ступенчатого массива с замeной первого элемента на максимальный: ");
            for (int i = 0; i < array.Length; i++)
            {
                int m = int.MinValue;
                int s = 0;
                for (int j = 0; j < array[i].Length; j++)
                { if (array[i][j] > m) { m = array[i][j]; s = j; } }
                int t = array[i][0];
                array[i][0] = m;
                array[i][s] = t;
            }
            foreach (int[] array2 in array)
            {
                foreach (int i in array2) { Console.Write(i + " "); }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("\nРеверс каждой строки ступенчатого массива: ");
            for (int i = 0; i < array.Length; i++)
            { Array.Reverse(array[i]); }
            foreach (int[] array2 in array) { foreach (int i in array2) { Console.Write(i + " "); } Console.WriteLine(); }
            Console.WriteLine();

            Console.WriteLine("\nПодсчет среднего значения в каждой строке: ");
            foreach (int[] array2 in array)
            { double average = array2.Average(); 
                Console.WriteLine(average); } 
            Console.WriteLine();
        }
    }
}