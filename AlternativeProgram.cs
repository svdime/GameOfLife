using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {
        public static void Paint(bool[,] field)
        {
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < field.GetLength(1); y++)
            {
                for (int x = 0; x < field.GetLength(0); x++)
                {
                    Console.ForegroundColor = field[x, y] ? ConsoleColor.Yellow : ConsoleColor.Blue;
                    var symbol = field[x, y] ? '#' : '.';
                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            CreateMap();
            ChooseCreatures();
            CreateCreatures();

            var field = Field.GameField;

            while (true)
            {
                Paint(field);
                Thread.Sleep(300);
                field = Game.NextStep(field);
            }
        }

        static void CreateMap()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Через пробел введите ширину и высоту игрового поля.\nP.S. 40x40 - оптимальное поле");
            Console.Write("Ширина и высота: ");
            var userData = Console.ReadLine().Trim().Split(' ');
            var width = int.Parse(userData[0]);
            var height = int.Parse(userData[1]);
            Console.Clear();
            Console.ResetColor();
            new Field(width, height);
            Field.GameField = new bool[Field.Width, Field.Height];
        }

        static void CreateCreatures()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Игровое поле {0}x{1}", Field.Width, Field.Height);
            Console.WriteLine("Введите клети, которые вы оживить / умертвить." +
                "\nВводить нужно 3 числа. Первое число отвечает за X, второе за Y, третье за состояние клетки (1 - клетка жива, любое другое число или символ - клетка мертва)" +
                "\nНапример 3 5 1 - оживит клетку на (X: 3, Y: 5)\nЧтобы прекратит селекцию напишите: Stop");
            while (true)
            {
                var userData = Console.ReadLine().Trim().ToLower();
                if (userData == "stop")
                    break;
                var newData = userData.Split(' ');
                var x = int.Parse(newData[0]);
                var y = int.Parse(newData[1]);
                var isAlive = newData[2] == "1" ? true : false;
                Field.GameField[x, y] = isAlive;
            }
            Console.ResetColor();
            Console.Clear();
        }
        
        static void ChooseCreatures()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Игровое поле {0}x{1}", Field.Width, Field.Height);
            Console.WriteLine("Выберете существ и их координаты.\nДоступные существа: Glider, Cycle, Block, SC.\nНапример: Glider 0 0");
            Console.WriteLine("Для остановки напишите: Stop\n");
            while (true)
            {          
                var data = Console.ReadLine().Trim().ToLower();
                if (data == "stop")
                    break;
                var parsedData = data.Split(' ');
                var name = parsedData[0];
                var creatureX = int.Parse(parsedData[1]);
                var creatureY = int.Parse(parsedData[2]);
                if (name == "cycle")
                    new Cycle(creatureX, creatureY);
                else if (name == "glider")
                    new Glider(creatureX, creatureY);
                else if (name == "block")
                    new Block(creatureX, creatureY);
                else if (name == "sc")
                    new StrangeCreature(creatureX, creatureY);
            }
            Console.Clear();
            Console.ResetColor();
        }

        public class Field
        {
            public static int Width { get; private set; }
            public static int Height { get; private set; }

            public static bool[,] GameField;

            public Field(int w, int h)
            {
                if (w <= 0)
                    throw new Exception("Недопустимое значение для ширины игрового поля");
                if (h <= 0)
                    throw new Exception("Недопустимое значение для высоты игрового поля");
                Width = w;
                Height = h;
            }
        }

        public class Glider
        {
            public readonly int X;
            public readonly int Y;

            public Glider(int x, int y)
            {
                if (x + 2 >= Field.Width || x < 0)
                    throw new Exception("Недопустимое значение x");
                if (y < 0 || y+2 >= Field.Height)
                    throw new Exception("Недопустимое значение y");

                X = x;
                Y = y + 2;

                Set(Field.GameField);
            }

            public void Set(bool[,] field)
            {
                for (var i = 0; i < 3; i++)
                    field[X + i, Y] = true;
                field[X + 1, Y - 2] = true;
                field[X + 2, Y - 1] = true;
            }
        }

        public class Cycle
        {
            public readonly int X;
            public readonly int Y;

            public Cycle(int x, int y)
            {
                if (x + 2 >= Field.Width || x < 0)
                    throw new Exception("Недопустимое значение x");
                if (y < 0 || y+2 >= Field.Height)
                    throw new Exception("Недопустимое значение y");

                X = x;
                Y = y + 1;

                Set(Field.GameField);
                
            }

            public void Set(bool[,] field)
            {
                for (var i = 0; i < 3; i++)
                    field[X + i, Y] = true;
            }
        }

        public class Block
        {
            public readonly int X;
            public readonly int Y;

            public Block(int x, int y)
            {
                if (x + 1 >= Field.Width || x < 0)
                    throw new Exception("Недопустимое значение x");
                if (y < 0 || y + 2 > Field.Height)
                    throw new Exception("Недопустимое значение y");

                X = x;
                Y = y;

                Set(Field.GameField);
            }

            public void Set(bool[,] field)
            {
                field[X, Y] = true;
                field[X, Y + 1] = true;
                field[X + 1, Y] = true;
                field[X + 1, Y + 1] = true;
            }
        }
        
        public class StrangeCreature
        {
            public readonly int X;
            public readonly int Y;

            public StrangeCreature(int x, int y)
            {
                if (x + 3 > Field.Width || x < 0)
                    throw new Exception("Недопустимое значение x");
                if (y < 0 || y+2 >= Field.Height)
                    throw new Exception("Недопустимое значение y");
                X = x;
                Y = y+2;

                Set(Field.GameField);
            }

            public void Set(bool[,] field)
            {
                field[X, Y] = true;
                for (var j = 0; j < 3; j++)
                    field[X + 1, Y - j] = true;
                field[X + 2, Y - 1] = true;
            }
        }
        
        public class FirsEater 
        {
            public readonly int X;
            public readonly int Y;

            public FirsEater(int x, int y)
            {
                throw new Exception("Тестовое существо");
                X = x;
                Y = y;
            }

            public void Set(bool[,] field)
            {
                field[X, Y - 2] = true;
                field[X, Y - 3] = true;
                field[X + 1, Y - 3] = true;
                for (var j = 0; j < 3; j++)
                    field[X + 2, Y - j] = true;
                field[X + 3, Y] = true;
            }
        }
    }
}
