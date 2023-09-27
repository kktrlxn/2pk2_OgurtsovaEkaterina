namespace PZ_06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();//рандом
            int[] array = new int[10];//создается массив
            for (int i = 0; i < array.Length; i++)//каждому элементу массива присваивается случайное число от 0 до 50
            { array[i] = rnd.Next(0, 51); }
            Array.Sort(array, (i, j) => GetDigitsSum(i).CompareTo(GetDigitsSum(j)));// массив сортируется в порядке возрастания суммы цифр

            static int GetDigitsSum(int number)//метод для вычисления суммы цифр числа
            {
                int sum = 0;
                while (number != 0)
                { sum += number % 10; number /= 10; }
                return sum;
            }

            foreach (int number in array)//вывод элементов массива с суммой их цифр
            {
                int sum = GetDigitsSum(number);
                Console.WriteLine($"{number} ( сумма цифр={sum})");
            }
        }
    }
}
