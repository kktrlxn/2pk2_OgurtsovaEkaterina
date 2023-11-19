namespace PZ_09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку в нотации camelCase: "); //ввод строки в нотации camelCase
            string input = Console.ReadLine();
            string output = string.Empty; //создание пустой строки для того, чтобы записать в нее полученное предложение 

            for (int i = 0; i < input.Length; i++)//проход по каждому символу введенной строки с помощью цикла и массива
            {
                char c = input[i];
                if (char.IsUpper(c) || char.IsDigit(c))//проверка, является ли символ заглавным или цифрой
                {
                    output += "_" + char.ToLower(c);//если буква заглавная или это цифра, то перед ставится нижнее подчеркивание, а буква переводится в нижний регистр
                }
                else
                {
                    output += c;//иначе символ записывается в пустую строку
                }
            }
            Console.WriteLine("Строка в нотации Underscore: " + output);//вывод строки в нотации Underscore
        }
    }
}