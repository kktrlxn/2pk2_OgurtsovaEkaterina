using System.Text;

namespace PZ_16
{
    internal class Program
    {
        //правка
        static int mapSize = 25; //размер карты
        static char[,] map; //карта

        static int playerY = mapSize / 2;   //координаты на карте игрока
        static int playerX = mapSize / 2;  //координаты на карте игрока
        static byte enemies = 10; //количество врагов
        static int countEnemies = 0;
        static int countBuff = 0;
        static int countHealth = 0;
        static byte buffs = 5; //количество усилений
        static int health = 5; // количество аптечек

        // настройки характеристик игрока и врага 
        public static int healthPlayer = 50;
        static int healthEnemies = 30;
        static int damagePlayer = 10;
        static int damageEnemies = 5;

        // максимальное здоровье для аптечки
        static int maxHealth = 50;

        // шаг
        static int countStep = 0;
        // шаг с баффом
        static int countBuffStep = 0;
        static string path;

        static List<int> enemyX = new List<int>();//координаты для сохранения врагов
        static List<int> enemyY = new List<int>();
        static int lex = 0;

        static List<int> buffsX = new List<int>();//координаты для сохранения баффов
        static List<int> buffsY = new List<int>();
        static int lbx = 0;

        static List<int> healthX = new List<int>();//координаты для сохранения аптечек
        static List<int> healthY = new List<int>();
        static int lhx = 0;

        static void Main(string[] args)
        {
            Prewie();
            Move();
        }

        static void Prewie()
        {
            Console.Clear();
            Console.SetCursorPosition(40, 15);
            Console.WriteLine("N - Начать новую игру");
            Console.SetCursorPosition(40, 16);
            Console.WriteLine("Z - Загрузить последнее сохранение");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.N: //запуск новой игры     
                    playerY = mapSize / 2;   //координаты на карте игрока
                    playerX = mapSize / 2;
                    countStep = 0;
                    healthPlayer = 50;
                    damagePlayer = 10;
                    enemies = 10; //количество врагов
                    countEnemies = 10;
                    buffs = 5; //количество усилений
                    countBuff = 0;
                    health = 5; // количество аптечек
                    GenerationMap();
                    break;
                case ConsoleKey.Z:
                    Console.Clear();
                    Console.SetCursorPosition(40, 15);
                    Console.Write("Название: ");
                    Load();
                    break;
                default: //если игрок нажимает на другие клавиши то стартовый экран не пропадает
                    Prewie();
                    break;
            }
        }
        static void GenerationMap()
        {
            Random random = new Random();
            map = new char[mapSize, mapSize];
            //создание пустой карты
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    map[i, j] = '_';
                }
            }
            map[playerY, playerX] = 'P'; // в cередину карты ставится игрок
            AddEnemies();//добавление врагов
            AddBuffs();//добавление баффов
            AddHealth();//добавление аптечек
            UpdateMap(); //отображение заполненной карты на консоли
        }

        static void AddEnemies()
        {
            Random random = new Random();
            while (enemies > 0)
            {
                int x = random.Next(1, mapSize);
                int y = random.Next(1, mapSize);

                //если ячейка пуста - туда добавляется враг
                if (map[x, y] == '_')
                {
                    enemyX.Add(x);
                    enemyY.Add(y);
                    lex++;
                    map[x, y] = 'E';
                    enemies--; //при добавлении врагов уменьшается количество нерасставленных врагов

                }
            }
        }

        static void AddBuffs()
        {
            Random random = new Random();
            while (buffs > 0)
            {
                int x = random.Next(1, mapSize);
                int y = random.Next(1, mapSize);

                if (map[x, y] == '_')
                {
                    buffsX.Add(x);
                    buffsY.Add(y);
                    lbx++;
                    countBuff++;
                    map[x, y] = 'B';
                    buffs--;
                }
            }
        }

        static void AddHealth()
        {
            Random random = new Random();
            while (health > 0)
            {
                int x = random.Next(1, mapSize);
                int y = random.Next(1, mapSize);

                if (map[x, y] == '_')
                {
                    healthX.Add(x);
                    healthY.Add(y);
                    lhx++;
                    countHealth++;
                    map[x, y] = 'H';
                    health--;
                }
            }
        }

        static void UpdateMap()
        {
            Console.Clear();
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    switch (map[i, j])
                    {
                        // вывод расскрашенных элементов
                        case 'H':

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(map[i, j]);
                            Console.ResetColor();
                            break;
                        case 'E':

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(map[i, j]);
                            Console.ResetColor();
                            break;
                        case 'B':

                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(map[i, j]);
                            Console.ResetColor();
                            break;
                        case 'P':

                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(map[i, j]);
                            Console.ResetColor();
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(map[i, j]);
                            Console.ResetColor();
                            break;
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(map[i, 0]);
                Console.ResetColor();
            }
        }

        static void Move()
        {
            while (true)
            {
                //предыдущие координаты игрока
                int playerOldY;
                int playerOldX;

                while (true)
                {
                    // время баффа
                    isBuffsStep();
                    Text();

                    playerOldX = playerX;
                    playerOldY = playerY;

                    //смена координат в зависимости от нажатия клавиш
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.UpArrow:
                            playerX--;
                            countStep++;
                            Text();
                            break;
                        case ConsoleKey.DownArrow:
                            playerX++;
                            countStep++;
                            Text();
                            break;
                        case ConsoleKey.LeftArrow:
                            playerY--;
                            countStep++;
                            Text();
                            break;
                        case ConsoleKey.RightArrow:
                            Text();
                            playerY++;
                            countStep++;
                            break;
                        case ConsoleKey.Q:
                            Pause();
                            break;
                    }
                    // ограничение области движения персонажа
                    Teleport();
                    // выбор проверки в зависимости от того, куда попал персонаж
                    switch (map[playerX, playerY])
                    {
                        case 'B':
                            CheckBuff();
                            break;
                        case 'H':
                            CheckHealth();
                            break;
                        case 'E':
                            CheckEnemies();
                            break;
                    }
                    Console.CursorVisible = false; //скрытный курсов

                    //предыдущее положение игрока затирается
                    map[playerOldY, playerOldX] = '_';

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(playerOldY, playerOldX);
                    Console.Write('_');
                    Console.ResetColor();

                    //обновленное положение игрока
                    map[playerY, playerX] = 'P';
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.SetCursorPosition(playerY, playerX);
                    Console.Write('P');
                    Console.ResetColor();
                    Text();
                }
            }
        }

        static void Pause()
        {
            //пауза
            Console.Clear();
            Console.SetCursorPosition(40, 15);
            Console.WriteLine("Q - Продолжить");
            Console.SetCursorPosition(40, 16);
            Console.WriteLine("N - Начать сначала");
            Console.SetCursorPosition(40, 17);
            Console.WriteLine("L - Сохранить игру");
            Console.SetCursorPosition(40, 18);
            Console.WriteLine("Z - Загрузить последнее сохранение");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Q:
                    Console.Clear();
                    UpdateMap();
                    Move();
                    break;
                case ConsoleKey.N: //запуск новой игры
                    playerY = mapSize / 2;   //координаты на карте игрока
                    playerX = mapSize / 2;
                    countStep = 0; // шаг
                    healthPlayer = 50; // здоровье игрока
                    damagePlayer = 10; // урон игрока
                    enemies = 10; //количество врагов
                    countEnemies = 10; //количество врагов в данный мамент
                    countBuff = 0;
                    countHealth = 0;
                    buffs = 5; //количество усилений
                    health = 5; // количество аптечек
                    GenerationMap();
                    break;
                case ConsoleKey.L: // сохранение в файл
                    Console.Clear();
                    Console.SetCursorPosition(40, 15);
                    Console.Write("Название: ");
                    Save();
                    Console.Clear();
                    Prewie();
                    break;
                case ConsoleKey.Z:
                    Console.Clear();
                    Console.SetCursorPosition(40, 15);
                    Console.Write("Название: ");
                    Load();
                    break;
                default: //если игрок нажимает на другие клавиши то стартовый экран не пропадает
                    Pause();
                    break;
            }
        }

        static void Load()
        {
            Console.CursorVisible = true;
            string path = Console.ReadLine();
            string path2 = path + 'w';
            try
            {
                using (FileStream file = new FileStream(path + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        // чтение из файла
                        string[] line = reader.ReadToEnd().Split('\n');
                        playerX = int.Parse(line[0]);
                        playerY = int.Parse(line[1]);
                        enemies = byte.Parse(line[2]);
                        buffs = byte.Parse(line[3]);
                        health = int.Parse(line[4]);
                        healthPlayer = int.Parse(line[5]);
                        damageEnemies = int.Parse(line[6]);
                        damagePlayer = int.Parse(line[7]);
                        countBuffStep = int.Parse(line[8]);
                        countStep = int.Parse(line[9]);
                        countEnemies = int.Parse(line[10]);
                        lex = int.Parse(line[11]);
                        lhx = int.Parse(line[12]);
                        lbx = int.Parse(line[13]);
                        countHealth = int.Parse(line[14]);
                        countBuff = int.Parse(line[15]);

                    }
                }
                using (FileStream file2 = new FileStream(path2 + ".txt", FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using (StreamReader reader1 = new StreamReader(file2))
                    {
                        // получение координат врагов 
                        Console.Clear();
                        string[] units = reader1.ReadToEnd().Split('\n');
                        int count = 0;
                        enemyX.Clear();
                        enemyY.Clear();
                        buffsX.Clear();
                        buffsY.Clear();
                        healthX.Clear();
                        healthY.Clear();
                        for (int i = 0; i < lex; i++) // координаты врагов
                        {
                            enemyX.Add(int.Parse(units[count]));
                            count++;
                        }
                        for (int i = 0; i < lex; i++)
                        {
                            enemyY.Add(int.Parse(units[count]));
                            count++;
                        }

                        for (int i = 0; i < lhx; i++) // координаты аптечек
                        {
                            healthX.Add(int.Parse(units[count]));
                            count++;
                        }
                        for (int i = 0; i < lhx; i++)
                        {
                            healthY.Add(int.Parse(units[count]));
                            count++;
                        }

                        for (int i = 0; i < lbx; i++) // координаты баффов
                        {
                            buffsX.Add(int.Parse(units[count]));
                            count++;
                        }
                        for (int i = 0; i < lbx; i++)
                        {
                            buffsY.Add(int.Parse(units[count]));
                            count++;
                        }
                        for (int i = 0; i < mapSize; i++)
                        {
                            for (int j = 0; j < mapSize; j++)
                            {
                                if (j == 24)
                                {
                                    Console.SetCursorPosition(0, 0);
                                }
                                else
                                {
                                    map[i, j] = '_';
                                }
                            }
                        }
                        //  выставление на карте
                        for (int i = 0; i < lex; i++)
                        {
                            map[enemyX[i], enemyY[i]] = 'o';
                        }
                        for (int i = 0; i < lbx; i++)
                        {
                            map[buffsX[i], buffsY[i]] = '^';
                        }
                        for (int i = 0; i < lhx; i++)
                        {
                            map[healthX[i], healthY[i]] = '+';
                        }
                        // обновление карты
                        UpdateMap();
                        map[playerX, playerY] = 'P';
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.SetCursorPosition(playerY, playerX);
                        Console.Write('P');
                        Console.ResetColor();
                        Move();
                    }
                }
            }
            catch
            {
                Console.CursorVisible = false;

                Console.SetCursorPosition(40, 15);
                Console.WriteLine("Такого сохранения нет.");
                Console.SetCursorPosition(40, 16);
                Console.WriteLine("Q - назад");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Q:
                        Console.Clear();
                        Prewie();
                        Move();
                        break;
                    default:
                        Console.SetCursorPosition(40, 15);
                        Console.Write("Название: ");
                        map[playerY, playerX] = '_';
                        Load();
                        break;
                }
            }
        }

        static void Save()
        {
            // сохранение в файл
            Console.CursorVisible = true;
            string path = Console.ReadLine();
            string path2 = path + 'w';
            using (FileStream file = new FileStream(path + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (StreamWriter writer = new StreamWriter(file))
                {
                    // сохраняем значения переменных
                    writer.WriteLine(playerX);
                    writer.WriteLine(playerY);
                    writer.WriteLine(enemies);
                    writer.WriteLine(buffs);
                    writer.WriteLine(health);
                    writer.WriteLine(healthPlayer);
                    writer.WriteLine(damageEnemies);
                    writer.WriteLine(damagePlayer);
                    writer.WriteLine(countBuffStep);
                    writer.WriteLine(countStep);
                    writer.WriteLine(countEnemies);
                    writer.WriteLine(lex);
                    writer.WriteLine(lhx);
                    writer.WriteLine(lbx);
                    writer.WriteLine(countHealth);
                    writer.WriteLine(countBuff);
                }
            }
            using (FileStream file2 = new FileStream(path2 + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (StreamWriter writer1 = new StreamWriter(file2))
                {
                    // сохранение координат 
                    for (int i = 0; i < enemyX.Count; i++) // врагов
                        writer1.WriteLine(enemyX[i]);
                    for (int i = 0; i < enemyY.Count; i++)
                        writer1.WriteLine(enemyY[i]);

                    for (int i = 0; i < healthX.Count; i++) // аптечек
                        writer1.WriteLine(healthX[i]);
                    for (int i = 0; i < healthY.Count; i++)
                        writer1.WriteLine(healthY[i]);

                    for (int i = 0; i < buffsX.Count; i++) //баффов
                        writer1.WriteLine(buffsX[i]);
                    for (int i = 0; i < buffsY.Count; i++)
                        writer1.WriteLine(buffsY[i]);
                }
            }
        }
        static void Teleport()
        {
            if (playerY == mapSize)
            {
                playerY = 0;
                countStep--;
            }
            else if (playerX == mapSize)
            {
                playerX = 0;
                countStep--;
            }
        }

        static void CheckHealth()
        {
            if (countHealth == 5)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(40, 11);
                Console.WriteLine("- Аптечка восстанавливает здоровье игрока на максимум");
                Console.ResetColor();
            }
            for (int i = 0; i < enemyX.Count; i++)
            {
                healthPlayer = maxHealth;
                map[playerX, playerY] = '_';
                healthX.Remove(i);
                healthY.Remove(i);
                lhx--;
                countHealth--;
                Text();
            }
        }

        static void CheckBuff()
        {
            if (countBuff == 5)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(40, 7);
                Console.WriteLine("- Бафф удваивает урон игрока на 20 шагов");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(40, 9);
                Console.Write($"- Осталось шагов до окончания действия баффа: {countBuffStep}" + " ");
            }
            for (int i = 0; i < enemyX.Count; i++)
            {
                if (map[playerX, playerY] == 'B')
                {
                    countBuffStep = 21;
                    damagePlayer = 20;
                    buffsX.Remove(i);
                    buffsY.Remove(i);
                    lbx--;
                    countBuff--;
                }
            }
        }

        static void CheckEnemies()
        {
            for (int i = 0; i < enemyX.Count; i++)
            {
                if (map[playerX, playerY] == 'E')
                {
                    while (healthPlayer > 0 && healthEnemies > 0)
                    {
                        if (countEnemies == 10)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(40, 5);
                            Console.WriteLine("- Враг - Урон: 5, Здоровье: 30)");
                            Console.ResetColor();

                        }
                        healthEnemies -= damagePlayer;
                        if (healthEnemies <= 0)
                        {
                            countEnemies--;
                            enemyY.RemoveAt(i);
                            enemyX.RemoveAt(i);
                            lex--;
                            if (countEnemies == 0)
                            {
                                WinText();
                            }
                            break;
                        }
                        healthPlayer -= damageEnemies;
                        if (healthPlayer <= 0)
                        {
                            Console.Clear();
                            countEnemies = 10;
                            EndText();
                        }
                    }
                    healthEnemies = 30;
                    map[playerX, playerY] = '_';
                }
            }
        }

        static void isBuffsStep()
        {
            //Время действия баффа
            if (damagePlayer >= 20)
            {
                countBuffStep -= 1;
                if (countBuffStep <= 0)
                {
                    damagePlayer = 10;
                }
            }
        }

        static void Text()
        {
            Console.SetCursorPosition(0, mapSize);
            Console.WriteLine($"Здоровье: {healthPlayer}" + " ");
            Console.WriteLine($"Шагов сделано: {countStep}");
            Console.WriteLine($"Сила удара игрока: {damagePlayer}");
            Console.WriteLine($"Врагов осталось: {10 - countEnemies}" + " ");
        }

        static void WinText()
        {
            Console.Clear();
            Console.SetCursorPosition(40, 15);
            Console.Write("Поздравляем! Вы убили всех!");
            Console.SetCursorPosition(40, 16);
            Console.WriteLine("N - Начать сначала");
            Console.SetCursorPosition(40, 17);
            Console.WriteLine("Z - Загрузить страое");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.N: //запуск новой игры
                    playerY = mapSize / 2;
                    playerX = mapSize / 2;
                    countStep = 0; // шаг
                    healthPlayer = 50; // жизни игрока
                    damagePlayer = 10; // урон игрока
                    enemies = 10; //количество врагов
                    countEnemies = 11; // количество врагов
                    countBuff = 0;
                    buffs = 5; //количество усилений
                    health = 5; // количество аптечек
                    GenerationMap();
                    break;
                case ConsoleKey.Z:
                    Console.Clear();
                    Console.SetCursorPosition(40, 15);
                    Console.Write("Название: ");
                    Load();
                    break;
                default:
                    WinText();
                    break;
            }
        }

        static void EndText()
        {
            Console.Clear();
            Console.SetCursorPosition(40, 15);
            Console.Write("Проигрыш.");
            Console.SetCursorPosition(40, 16);
            Console.WriteLine("N - Начать сначала");
            Console.SetCursorPosition(40, 17);
            Console.WriteLine("Z - Загрузить старое");
            switch (Console.ReadKey().Key)
            {

                case ConsoleKey.N: //запуск новой игры
                    playerY = mapSize / 2;
                    playerX = mapSize / 2;
                    countStep = 0;
                    healthPlayer = 50;
                    damagePlayer = 10;
                    enemies = 10; //количество врагов
                    countEnemies = 11;
                    countBuff = 0;
                    buffs = 5; //количество усилений
                    health = 5; // количество аптечек
                    GenerationMap();
                    break;
                case ConsoleKey.L: // сохранение в файл
                    Console.Clear();
                    Console.SetCursorPosition(40, 15);
                    Console.Write("Название: ");
                    Save();
                    Console.Clear();
                    Prewie();
                    break;
                case ConsoleKey.Z:
                    Console.Clear();
                    Console.SetCursorPosition(40, 15);
                    Console.Write("Название: ");
                    Load();
                    break;
                default:
                    WinText();
                    break;
            }
        }
    }
}
