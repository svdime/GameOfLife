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
        public const int Width = 50;
        public const int Height = 50;

        public static void Paint(bool[,] field)
        {
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < field.GetLength(1); y++)
            {
                for (int x = 0; x < field.GetLength(0); x++)
                {
                    var symbol = field[x, y] ? '#' : ' ';
                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            var field = new bool[Width, Height];

            var strangeCreature = new StrangeCreature(15, 15);
            strangeCreature.Set(field);


            while (true)
            {
                Paint(field);
                Thread.Sleep(500);
                field = Game.NextStep(field);
            }
        }

        /*
        Ниже придествлены классы, которые позволяют создать некоторых существ
        P.S. X и Y задают начально положение существа, в свою очередь метод Set
        будет определять остальные клетки. 
        Порядок вызова: 1. Создать объект в Main; 2. У этого объекта вызвать метод Set(field).
        P.P.S Вероятно следует поправить вызовы ошибок.
        */

        public class Glider
        {
            public readonly int X;
            public readonly int Y;

            public Glider(int x, int y)
            {
                if (x + 2 >= Width || x < 0)
                    throw new Exception("Недопустимое значение x");
                if (y - 2 < 0 || y > Height)
                    throw new Exception("Недопустимое значение y");

                X = x;
                Y = y;
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
                if (x + 2 >= Width || x < 0)
                    throw new Exception("Недопустимое значение x");
                if (y - 2 < 0 || y > Height)
                    throw new Exception("Недопустимое значение y");

                X = x;
                Y = y;
                
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
                if (x + 1 >= Width || x < 0)
                    throw new Exception("Недопустимое значение x");
                if (y < 0 || y + 2 > Height)
                    throw new Exception("Недопустимое значение y");

                X = x;
                Y = y;
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
                if (x - 2 >= Width || x < 0)
                    throw new Exception("Недопустимое значение x");
                if (y - 2 < 0 || y > Height)
                    throw new Exception("Недопустимое значение y");
                X = x;
                Y = y;
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
