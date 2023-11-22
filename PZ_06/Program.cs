namespace PZ_06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();//рандом
            int[] array = new int[10];//создается массив
            for (int i = 0; i < array.Length; i++)
            { array[i] = rnd.Next(0, 51); }//каждому элементу массива присваивается случайное число от 0 до 50
            int[] array2 = new int[10];//второй массив для записи в него суммы цифр числа
            for (int i = 0; i < array2.Length; i++)//цикл для заполнения второго массива
            {
                int number = array[i];//число берется из первого массива
                int sum = 0;

                while (number > 0)//цикл выполняется для всех чисел больше 0
                {
                    sum += number % 10;//сумма будет равна сумме остатка от деления на 10 и числа поделенного на 10 без остатка
                    number /= 10;
                }
                array2[i] = sum;//заполнение массива
            }
            Array.Sort(array2, array);//сортировка массива по возрастанию суммы чисел
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine($"Сумма цифр числа {array[i]} равна {array2[i]}");//вывод
            }
        }
    }
}
