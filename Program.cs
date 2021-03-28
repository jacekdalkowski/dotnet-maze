using System;

namespace maze
{
    class Program
    {
        static void Main(string[] args)
        {
            var maze = Puzzle.GetMaze1();
            var shortestPathLength = BFSSolver.ComputeShortestPathLength(maze);
            Console.WriteLine($"shortestPathLength: {shortestPathLength}");
        }
    }
}
