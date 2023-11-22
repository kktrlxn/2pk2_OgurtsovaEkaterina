namespace PZ_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите ФИО студента, предметы и оценки за год по данным предметам: ");//ввод текста с консоли
            string text = Console.ReadLine();
            string[] data = text.Split(" ");//разбиение строки по пробелу и внесение подстрок в массив

            int[] marks = new int[data.Length];//создание массива равного длине первого массива со строкой
            for (int i = 0; i < data.Length; i++)//проход по массиву для поиска в нем чисел
            {
                if (int.TryParse(data[i], out int mark))//каждый элемент массива приводится к типу int
                {
                    marks[i] = mark;//если переменная типа int, то она записывается во второй массив
                }
            }
            int sum = 0;//инициализация переменных для подсчета суммы и количестива чисел
            int count = 0;
            for (int i = 0; i < marks.Length; i++)//проход по массиву
            {
                sum += marks[i];
                count++;
            }
            double average = (double)sum / count;//вычисление среднего арифметического
            Console.WriteLine(average);//вывод на консоль
        }
    }
}