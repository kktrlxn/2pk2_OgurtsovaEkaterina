using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace PZ_16
{
    internal class Program
    {
        static int mapSize = 25; //размер карты
        static char[,] map = new char[mapSize, mapSize]; //карта
        //координаты на карте игрока
        static int playerY = mapSize / 2;
        static int playerX = mapSize / 2;
        static byte enemies = 10; //количество врагов
        static byte buffs = 5; //количество усилений
        static int health = 5;  // количество аптечек
        static int hp = 50;// здоровье игрока
        static int dp = 5;// урон игрока
        static int dmg = 5; // урон врага
        static int healthEnemies = 15; // здоровье врага
        static int countWithBuff = 0;// шаги под баффом
        static bool buffOn = false;
        static bool game = true;
        static int winPoint = enemies;

        static List<int> enemyX = new List<int>();//координаты для врагов
        static List<int> enemyY = new List<int>();
        // для проверки сколько осталось врагов для отбражения их на карте
        static int kolEnemyForList = 0;

        static List<int> buffsX = new List<int>();//координаты для баффов
        static List<int> buffsY = new List<int>();
        static int kolBuffForList = 0;

        static List<int> healthX = new List<int>();//координаты для хилок
        static List<int> healthY = new List<int>();
        static int kolHealthForList = 0;
        static int Count = 0; // бафф
        static void Main(string[] args)
        {
            HomeScreen();
            Move();
        }
        static void HomeScreen()
        {//стартовый экран
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(playerY, playerX);
            Console.SetCursorPosition(40, 15);
            Console.WriteLine("N - Новая игра");
            Console.SetCursorPosition(40, 16);
            Console.WriteLine("Z - Загрузить последнее сохранение");
            Console.ResetColor();

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.N: //запуск новой игры     
                    playerY = mapSize / 2;
                    playerX = mapSize / 2;
                    Count = 0;
                    hp = 50;
                    dp = 10;
                    enemies = 10;
                    buffs = 5;
                    countWithBuff = 0;
                    health = 5;
                    kolEnemyForList = 0;
                    kolBuffForList = 0;
                    kolHealthForList = 0;
                    winPoint = enemies;
                    Text();
                    GenerationMap();
                    break;
                case ConsoleKey.Z:
                    Console.Clear();
                    Console.SetCursorPosition(40, 15);
                    Console.Write("Название: ");
                    Load();
                    break;
                default:
                    HomeScreen();
                    break;
            }
        }
        static void GenerationMap()
        {
            Random random = new Random();
            //создание пустой карты
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
            map[playerY, playerX] = 'P'; // в cередину карты ставится игрок
            //временные координаты для проверки занятости ячейки
            int x;
            int y;
            //добавление врагов
            while (enemies > 0)
            {
                x = random.Next(0, mapSize);
                y = random.Next(0, mapSize);
                //если ячейка пуста  - туда добавляется враг
                if (map[x, y] == '_' && y != 0)
                {
                    enemyX.Add(x);
                    enemyY.Add(y);
                    kolEnemyForList++;
                    map[x, y] = 'E';
                    enemies--; //при добавлении врагов уменьшается количество нерасставленных врагов
                }
            }
            //добавление баффов
            while (buffs > 0)
            {
                x = random.Next(0, mapSize);
                y = random.Next(0, mapSize);

                if (map[x, y] == '_' && y != 0)
                {
                    buffsX.Add(x);
                    buffsY.Add(y);
                    kolBuffForList++;
                    map[x, y] = 'B';
                    buffs--;
                }
            }
            //добавление аптечек
            while (health > 0)
            {
                x = random.Next(0, mapSize);
                y = random.Next(0, mapSize);

                if (map[x, y] == '_' && y != 0)
                {
                    healthX.Add(x);
                    healthY.Add(y);
                    kolHealthForList++;
                    map[x, y] = 'H';
                    health--;
                }

            }
            //отображение заполненной карты на консоли
            UpdateMap();
        }
        static void UpdateMap()
        {//обновление карты
            Console.Clear();
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    switch (map[i, j])
                    {
                        case 'H':
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(map[i, j]);
                            Console.ResetColor();
                            break;
                        case 'B':
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(map[i, j]);
                            Console.ResetColor();
                            break;
                        case 'E':
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(map[i, j]);
                            Console.ResetColor();
                            break;
                        case 'P':
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(map[i, j]);
                            Console.ResetColor();
                            break;
                        default:
                            Console.Write(map[i, j]);
                            break;
                    }
                }
                Console.WriteLine(map[i, 0]);
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(40, 5);
            Console.WriteLine("E - Враг Урон: 5, Здоровье: 30");
            Console.ResetColor();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(40, 7);
            Console.WriteLine("B - Бафф удваивает урон игрока на 20 шагов");
            Console.ResetColor();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(40, 9);
            Console.WriteLine("H - Аптечка восстанавливает здоровье игрока на максимум");
            Console.ResetColor();
        }
        static void Move()
        {//перемещение по карте
            //предыдущие координаты игрока
            int playerOldX;
            int playerOldY;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(40, 3);
            Console.WriteLine("P - Игрок");
            Console.ResetColor();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(40, 5);
            Console.WriteLine("E - Враг Урон: 5, Здоровье: 15");
            Console.ResetColor();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(40, 7);
            Console.WriteLine("B - Бафф удваивает урон игрока на 20 шагов");
            Console.ResetColor();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(40, 9);
            Console.WriteLine("H - Аптечка восстанавливает здоровье игрока на максимум");
            Console.ResetColor();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(40, 11);
            Console.WriteLine("Q - Меню");
            Console.ResetColor();
            while (true)
            {
                playerOldX = playerX;
                playerOldY = playerY;
                //смена координат в зависимости от нажатия клавиш
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        playerX--;
                        Count++;
                        Fight();
                        Text();
                        break;
                    case ConsoleKey.DownArrow:
                        playerX++;
                        Count++;
                        Fight();
                        Text();
                        break;
                    case ConsoleKey.LeftArrow:
                        playerY--;
                        Count++;
                        Fight();
                        Text();
                        break;
                    case ConsoleKey.RightArrow:
                        playerY++;
                        Count++;
                        Fight();
                        Text();
                        break;
                    case ConsoleKey.Q:
                        Console.Clear();
                        Count--;
                        Menu();
                        break;
                }
                if (playerY == mapSize)//телепорт
                {
                    playerY = 0;
                    Count--;
                    if (buffOn)
                    {
                        countWithBuff++;
                    }
                }
                if (playerX == -1)
                {
                    playerX = mapSize - 1;
                    Count--;
                    if (buffOn)
                    {
                        countWithBuff++;
                    }
                }
                if (playerY == -1)
                {
                    playerY = mapSize - 1;
                    Count--;
                    if (buffOn)
                    {
                        countWithBuff++;
                    }
                }
                if (playerX == mapSize)
                {
                    playerX = 0;
                    Count--;
                    if (buffOn)
                    {
                        countWithBuff++;
                    }
                }
                if (hp <= 0)//проигрыш
                {
                    End();
                }
                if (winPoint == 0)//выигрыш
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(40, 15);
                    Console.Write($"Вы Выиграли!");
                    Console.SetCursorPosition(40, 16);
                    Console.Write($"N - Новая игра");
                    Console.SetCursorPosition(40, 17);
                    Console.Write($"Z - Загрузить последнее сохранение");
                    Console.ResetColor();

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.N:
                            mapSize = 25; //размер карты
                            map = new char[mapSize, mapSize]; //карта
                                                              //координаты на карте игрока
                            playerY = mapSize / 2;
                            playerX = mapSize / 2;
                            enemies = 10; //количество врагов
                            buffs = 5; //количество усилений
                            health = 5;  // количество аптечек
                            hp = 50;//здоровье игрока
                            dmg = 10;
                            dp = 5;//здоровье игрока
                            countWithBuff = 0;//здоровье игрока
                            buffOn = false;
                            Count = 0;
                            winPoint = enemies;

                            enemyX = new List<int>();//координаты для врагов
                            enemyY = new List<int>();

                            buffsX = new List<int>();//координаты для бафов
                            buffsY = new List<int>();

                            healthX = new List<int>();//координаты для хилок
                            healthY = new List<int>();
                            GenerationMap();
                            break;
                        case ConsoleKey.Z:
                            Console.Clear();
                            Console.SetCursorPosition(40, 15);
                            Console.Write("Название: ");
                            Load();
                            break;
                        default: //если игрок нажимает на другие клавиши то стартовый экран не пропадает
                            HomeScreen();
                            break;
                    }
                }
                Console.CursorVisible = false; //скрытный курсив
                //предыдущее положение игрока затирается
                map[playerOldX, playerOldY] = '_';
                Console.SetCursorPosition(playerOldY, playerOldX);
                Console.Write('_');
                //обновление положения персонажа
                map[playerX, playerY] = 'P';
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.SetCursorPosition(playerY, playerX);
                Console.WriteLine('P');
                Console.ResetColor();
            }
        }
        static void Fight()
        {
            for (int i = 0; i < enemyY.Count; i++)
            {
                if (playerY == enemyY[i] && playerX == enemyX[i])
                {
                    hp -= dmg;
                    Text();
                    Count--;
                    enemyY.RemoveAt(i);
                    enemyX.RemoveAt(i);
                    kolEnemyForList--;
                    winPoint--;
                }
            }
            for (int i = 0; i < buffsX.Count; i++)
            {
                if (playerY == buffsY[i] && playerX == buffsX[i])
                {
                    buffsX.RemoveAt(i);
                    buffsY.RemoveAt(i);
                    kolBuffForList--;
                    countWithBuff += 20;
                    Text();
                    Count--;
                    if (countWithBuff > 0)
                    {
                        buffOn = true;
                        dmg = 5;
                        dp = 10;
                    }
                }
            }
            if (countWithBuff == 0)
            {
                buffOn = false;
                dmg = 15;
                dp = 5;
            }
            if (buffOn == true)
            {
                countWithBuff--;
                Console.SetCursorPosition(40, 23);
                Console.WriteLine($"Осталось шагов под баффом: {countWithBuff}" + " ");
            }
            for (int i = 0; i < healthX.Count; i++)
            {
                if (playerY == healthY[i] && playerX == healthX[i])
                {
                    hp = 50;
                    Text();
                    Count--;
                    healthX.RemoveAt(i);
                    healthY.RemoveAt(i);
                    kolHealthForList--;
                }
            }
        }
        static void Text()
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(0, mapSize);
                Console.WriteLine($"Здоровье: {hp}" + " ");
                Console.WriteLine($"Шагов пройдено: {Count}" + " ");
                Console.WriteLine($"Сила удара: {dp}" + " ");
                Console.WriteLine($"Враги: {kolEnemyForList}" + " ");
                Console.ResetColor();
            }
        static void Menu()
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(40, 15);
                Console.WriteLine("Q - Назад");
                Console.SetCursorPosition(40, 16);
                Console.WriteLine("N - Новая игра");
                Console.SetCursorPosition(40, 17);
                Console.WriteLine("L - Сохранить игру");
                Console.SetCursorPosition(40, 18);
                Console.WriteLine("Z - Загрузить последнее сохранение");
                Console.ResetColor();
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Q:
                        Console.Clear();
                        Count--;
                        UpdateMap();
                        Move();
                        break;
                    case ConsoleKey.N: //запуск новой игры
                        playerY = mapSize / 2;   //координаты на карте игрока
                        playerX = mapSize / 2;
                        Count = 0; // шаг
                        hp = 50; // здоровье игрока
                        dp = 10; // урон игрока
                        enemies = 10; //количество врагов
                        kolEnemyForList = 0; //количество врагов в данный мамент
                        kolBuffForList = 0;
                        kolHealthForList = 0;
                        buffs = 5; //количество усилений
                        health = 5; // количество аптечек
                        winPoint = enemies;

                        enemyX = new List<int>();//координаты для врагов
                        enemyY = new List<int>();
                        buffsX = new List<int>();//координаты для бафов
                        buffsY = new List<int>();
                        healthX = new List<int>();//координаты для хилок
                        healthY = new List<int>();
                        GenerationMap();
                        Text();
                        break;
                    case ConsoleKey.L: // сохранение в файл
                        Console.Clear();
                        Console.SetCursorPosition(40, 15);
                        Console.Write("Название: ");
                        Save();
                        Console.Clear();
                        HomeScreen();
                        break;
                    case ConsoleKey.Z:
                        Console.Clear();
                        Console.SetCursorPosition(40, 15);
                        Console.Write("Название: ");
                        Load();
                        break;
                    default: //если игрок нажимает на другие клавиши то стартовый экран не пропадает
                        Menu();
                        break;
                }
            }// пауза
        static void Win()
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(40, 15);
                Console.Write("Вы выиграли!");
                Console.SetCursorPosition(40, 16);
                Console.WriteLine("N - Новая игра");
                Console.SetCursorPosition(40, 17);
                Console.WriteLine("Z - Загрузить последнее сохранение");
                Console.ResetColor();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.N: //запуск новой игры

                        playerY = mapSize / 2;
                        playerX = mapSize / 2;
                        Count = 0; // шаг
                        hp = 50; // жизни игрока
                        dp = 10; // урон игрока
                        enemies = 10; //количество врагов
                        kolEnemyForList = 0; //количество врагов в данный мамент
                        kolBuffForList = 0;
                        kolHealthForList = 0;
                        countWithBuff = 0;
                        buffs = 5; //количество усилений
                        health = 5; // количество аптечек
                        winPoint = enemies;

                        enemyX = new List<int>();//координаты для врагов
                        enemyY = new List<int>();

                        buffsX = new List<int>();//координаты для бафов
                        buffsY = new List<int>();

                        healthX = new List<int>();//координаты для хилок
                        healthY = new List<int>();
                        GenerationMap();
                        Text();
                        break;
                    case ConsoleKey.Z:
                        Console.Clear();
                        Console.SetCursorPosition(40, 15);
                        Console.Write("Название: ");
                        Load();
                        break;
                    default:
                        Win();
                        break;
                }
            }
        static void End()
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(40, 15);
                Console.Write("Вы проиграли!");
                Console.SetCursorPosition(40, 16);
                Console.WriteLine("N - Новая игра");
                Console.SetCursorPosition(40, 17);
                Console.WriteLine("Z - Загрузить последнее сохранение");
                Console.ResetColor();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.N: //запуск новой игры
                        playerY = mapSize / 2;
                        playerX = mapSize / 2;
                        Count = 0; // шаг
                        hp = 50; // жизни игрока
                        dp = 10; // урон игрока
                        enemies = 10; //количество врагов
                        kolEnemyForList = 0; //количество врагов в данный мамент
                        kolBuffForList = 0;
                        kolHealthForList = 0;
                        countWithBuff = 0;
                        buffs = 5; //количество усилений
                        health = 5; // количество аптечек
                        winPoint = enemies;

                        enemyX = new List<int>();//координаты для врагов
                        enemyY = new List<int>();

                        buffsX = new List<int>();//координаты для бафов
                        buffsY = new List<int>();

                        healthX = new List<int>();//координаты для хилок
                        healthY = new List<int>();
                        GenerationMap();
                        Text();
                        break;
                    case ConsoleKey.Z:
                        Console.Clear();
                        Console.SetCursorPosition(40, 15);
                        Console.Write("Название: ");
                        Load();
                        break;
                    default:
                        End();
                        break;
                }
            }
        static void StartGame()
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(40, 15);
                Console.WriteLine("N - Новая игра");
                Console.SetCursorPosition(40, 16);
                Console.WriteLine("Z - Загрузить последнее сохранение");
                Console.ResetColor();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.N:
                        mapSize = 25; //размер карты
                        map = new char[mapSize, mapSize]; //карта
                        playerY = mapSize / 2;//координаты на карте игрока
                        playerX = mapSize / 2;
                        enemies = 10; //количество врагов
                        buffs = 5; //количество усилений
                        health = 5;  // количество аптечек
                        hp = 50;//здоровье игрока
                        dmg = 10;
                        dp = 5;//здоровье игрока
                        countWithBuff = 0;//здоровье игрока
                        buffOn = false;
                        Count = 0;
                        GenerationMap();
                        Text();
                        Move();
                        break;
                    case ConsoleKey.Z:
                        Console.Clear();
                        Console.SetCursorPosition(40, 15);
                        Console.Write("Название: ");
                        map[playerY, playerX] = '_';
                        Load();
                        break;
                    default:
                        StartGame();
                        break;
                }

            }//старт игры - новая или восстановление
        static void Save()
            {
                Console.CursorVisible = true;
                string path = Console.ReadLine();
                string path2 = path + '1';

                using (FileStream file = new FileStream(path + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        writer.WriteLine(playerX);
                        writer.WriteLine(playerY);
                        writer.WriteLine(enemies);
                        writer.WriteLine(buffs);
                        writer.WriteLine(health);
                        writer.WriteLine(hp);
                        writer.WriteLine(dmg);
                        writer.WriteLine(dp);
                        writer.WriteLine(countWithBuff);
                        writer.WriteLine(Count);
                        writer.WriteLine(winPoint);
                        writer.WriteLine(kolEnemyForList);
                        writer.WriteLine(kolHealthForList);
                        writer.WriteLine(kolBuffForList);
                    }
                }
                using (FileStream file2 = new FileStream(path2 + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamWriter writer1 = new StreamWriter(file2))
                    {
                        for (int i = 0; i < enemyX.Count; i++)//враги
                            writer1.WriteLine(enemyX[i]);
                        for (int i = 0; i < enemyY.Count; i++)
                            writer1.WriteLine(enemyY[i]);

                        for (int i = 0; i < healthX.Count; i++)//аптечка
                            writer1.WriteLine(healthX[i]);
                        for (int i = 0; i < healthY.Count; i++)
                            writer1.WriteLine(healthY[i]);

                        for (int i = 0; i < buffsX.Count; i++)//баф
                            writer1.WriteLine(buffsX[i]);
                        for (int i = 0; i < buffsY.Count; i++)
                            writer1.WriteLine(buffsY[i]);
                    }
                }
            }
        static void Load()
            {
                Console.CursorVisible = true;
                string path = Console.ReadLine();
                string path2 = path + '1';

                try
                {
                    using (FileStream file = new FileStream(path + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamReader reader = new StreamReader(file))
                        {
                            string[] line = reader.ReadToEnd().Split('\n');
                            playerX = int.Parse(line[0]);
                            playerY = int.Parse(line[1]);
                            enemies = byte.Parse(line[2]);
                            buffs = byte.Parse(line[3]);
                            health = int.Parse(line[4]);
                            hp = int.Parse(line[5]);
                            dmg = int.Parse(line[6]);
                            dp = int.Parse(line[7]);
                            countWithBuff = int.Parse(line[8]);
                            Count = int.Parse(line[9]);
                            winPoint = int.Parse(line[10]);
                            kolEnemyForList = int.Parse(line[11]);
                            kolHealthForList = int.Parse(line[12]);
                            kolBuffForList = int.Parse(line[13]);

                        }
                    }
                    using (FileStream file2 = new FileStream(path2 + ".txt", FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        using (StreamReader reader1 = new StreamReader(file2))
                        {
                            Console.Clear();
                            string[] units = reader1.ReadToEnd().Split('\n');
                            int count = 0;
                            enemyX.Clear();
                            enemyY.Clear();
                            buffsX.Clear();
                            buffsY.Clear();
                            healthX.Clear();
                            healthY.Clear();
                            for (int i = 0; i < kolEnemyForList; i++)//враги
                            {
                                enemyX.Add(int.Parse(units[count]));
                                count++;
                            }
                            for (int i = 0; i < kolEnemyForList; i++)
                            {
                                enemyY.Add(int.Parse(units[count]));
                                count++;
                            }

                            for (int i = 0; i < kolHealthForList; i++)//аптечка
                            {
                                healthX.Add(int.Parse(units[count]));
                                count++;
                            }
                            for (int i = 0; i < kolHealthForList; i++)
                            {
                                healthY.Add(int.Parse(units[count]));
                                count++;
                            }

                            for (int i = 0; i < kolBuffForList; i++)//баффы
                            {
                                buffsX.Add(int.Parse(units[count]));
                                count++;
                            }
                            for (int i = 0; i < kolBuffForList; i++)
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
                            for (int i = 0; i < kolEnemyForList; i++)
                            {
                                map[enemyX[i], enemyY[i]] = 'E';
                            }
                            for (int i = 0; i < kolBuffForList; i++)
                            {
                                map[buffsX[i], buffsY[i]] = 'B';
                            }
                            for (int i = 0; i < kolHealthForList; i++)
                            {
                                map[healthX[i], healthY[i]] = 'H';
                            }
                            UpdateMap();
                            map[playerX, playerY] = 'P';
                            Console.BackgroundColor = ConsoleColor.Black;
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
                    string start = "N - Новая игра";
                    Console.CursorVisible = false;
                    int centerX = (Console.WindowWidth / 2) - (start.Length / 2);
                    int centerY = (Console.WindowHeight / 2) - 1;
                    Console.SetCursorPosition(centerX, centerY);
                    Console.WriteLine("Такого сохранения нет... Нажмите Q.");
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.Q:
                            Console.Clear();
                            StartGame();
                            break;
                        default:
                            Console.Clear();
                            StartGame();
                            break;
                    }
                }
        }
    }
}
