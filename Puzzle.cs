using System;

namespace maze
{
    class Puzzle
    {
        public static int[,] GetMaze1()
        {
            int[,] maze = new int[,] 
            {
                { 0, 0, 1, 0, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0, 1, 0, 0 },
                { 0, 0, 1, 0, 0, 1, 0, 0 },
                { 0, 0, 1, 0, 0, 1, 0, 0 },
                { 0, 0, 0, 0, 0, 1, 0, 0 }
            };

            return maze;
        }
    }
}
