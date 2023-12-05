namespace PZ_15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dirName = @"C:\Program Files";
            if (Directory.Exists(dirName))//проверка на существование
            {
                string[] dirs = Directory.GetDirectories(dirName);//если путь существует, то название подкаталогов добавятся в строковый масисив
                if (dirs.Length == 0)//если в массив ничего не записалось, тк каталог пуст, то будет выведено соответствующее сообщение
                {
                    Console.WriteLine("Подкаталогов нет");
                }
                else
                {
                    Console.WriteLine("Отсортированные подкаталоги: ");
                    Array.Sort(dirs, (x, y) => y.Length.CompareTo(x.Length));//иначе массив сортируется по уменьшению длины названия каталога с помощью лямбда-выражения
                    foreach (string str in dirs)//проход по массиву и его вывод на консоль
                    {
                        Console.WriteLine(str);
                    }
                }
            }
        }
    }
}