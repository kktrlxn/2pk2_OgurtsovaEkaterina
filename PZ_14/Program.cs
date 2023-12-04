using System.Text;

namespace PZ_14
{
    class Program
    {
        static void Main(string[] args)
        {//был создан файл file.txt с текстом русского алфавита в обратном порядке
            try
            {
                string path = "file.txt";//название файла
                string[] lines = File.ReadAllLines(path);//чтение файла
                using (StreamWriter writer = new StreamWriter(path, false, Encoding.Default))//создание потока для записи
                {
                    foreach (string line in lines)
                    {
                        char[] chars = line.ToCharArray();//преобразование строки в массив
                        Array.Sort(chars);//сортировка массива символов
                        writer.WriteLine(new string(chars));//запись отсортированной строки в файл
                    }
                }

                Console.WriteLine("Сортировка символов выполнена.");//в файле будет записан алфавит
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.ReadLine();
        }
    }
}