using System.Collections.Generic;

namespace GameOfLife
{
    public class Game
    {
        public static bool[,] NextStep(bool[,] field)
        {
            var width = field.GetLength(0);
            var height = field.GetLength(1);
            var newGeration = new bool[width, height];
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                {
                    var amountNeighbours = CountNeighbours(x, y, field, width, height);
                    if (amountNeighbours < 2 || amountNeighbours > 3)
                        newGeration[x, y] = false;
                    else if (amountNeighbours == 3)
                        newGeration[x, y] = true;
                    else                   
                        newGeration[x, y] = field[x, y];
                }
            return newGeration;
        }

        static int CountNeighbours(int x, int y, bool[,] field, int width, int height)
        {
            var counter = 0;
            for (var i = -1; i < 2; i++)
                for (var j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0) continue;
                    if (IsInsideBorders(x + i, y + j, width, height))
                        if (field[x + i, y + j])
                            counter++;
                }
            return counter;
        }

        static bool IsInsideBorders(int x, int y, int width, int height)
        {
            return (x > -1 && y > -1) && (x < width && y < height);
        }
    }
}