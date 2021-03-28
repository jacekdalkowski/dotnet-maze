using System;
using System.Collections.Generic;
using System.Linq;

namespace maze
{
    class BFSSolver
    {
        class BFSCell
        {
            public readonly bool IsFloor;
            public readonly int Row;
            public readonly int Col;
            public CellState State { get; set; }
            public int DistanceFromStart { get; set; }

            public enum CellState 
            {
                NOT_DISCOVERED, DISCOVERED, VISITED
            }

            public BFSCell(bool isFloor, int row, int col) => (IsFloor, Row, Col) = (isFloor, row, col);

            public override string ToString() => IsFloor ? "_" : "X";
        }

        class BFSMaze 
        {
            public readonly BFSCell[,] _cells;
            public BFSCell[,] Cells => _cells;
            
            public BFSMaze(int[,] rawMaze)
            {
                _cells = new BFSCell[rawMaze.GetLength(0), rawMaze.GetLength(1)];
                for (int r = 0; r < rawMaze.GetLength(0); r++)
                {
                    for (int c = 0; c < rawMaze.GetLength(1); c++)
                    {
                        _cells[r, c] = new BFSCell(rawMaze[r, c] == 0, r, c);
                    }
                }
            }

            public override string ToString()
            {
                var maze = string.Empty;
                for (int r = 0; r < _cells.GetLength(0); r++)
                {
                    var row = string.Empty;
                    for (int c = 0; c < _cells.GetLength(1); c++)
                    {
                        row += _cells[r, c] + " ";
                    }
                    row += Environment.NewLine;
                    maze += row;
                }

                return maze;
            }
        }


        public static int ComputeShortestPathLength(int[,] rawMaze)
        {
            var bfsMaze = new BFSMaze(rawMaze);
            var q = new Queue<BFSCell>();
            q.Enqueue(bfsMaze.Cells[0, 0]);
            bfsMaze.Cells[0, 0].State = BFSCell.CellState.DISCOVERED;
            bfsMaze.Cells[0, 0].DistanceFromStart = 0;
            while (q.Count > 0) 
            {
                var cellToVisit = q.Dequeue();
                var undiscoveredNeighbours = FindUndiscoveredNeighbours(bfsMaze, cellToVisit);
                undiscoveredNeighbours.ToList().ForEach(un => un.State = BFSCell.CellState.DISCOVERED);
                undiscoveredNeighbours.ToList().ForEach(un => un.DistanceFromStart = cellToVisit.DistanceFromStart + 1);
                undiscoveredNeighbours.ToList().ForEach(un => q.Enqueue(un));
                cellToVisit.State = BFSCell.CellState.VISITED;
                if (cellToVisit.Row == bfsMaze.Cells.GetLength(0) - 1
                        && cellToVisit.Col == bfsMaze.Cells.GetLength(1) - 1)
                {
                    return cellToVisit.DistanceFromStart;
                }
            }
            Console.WriteLine(bfsMaze);
            return 0;
        }

        private static IEnumerable<BFSCell> FindUndiscoveredNeighbours(BFSMaze maze, BFSCell cell)
        {
            var undiscoveredNeighbours = new List<BFSCell>();

            if (cell.Row > 0 
                    && maze.Cells[cell.Row - 1, cell.Col] is var upperCandidate 
                    && upperCandidate.IsFloor
                    && upperCandidate.State == BFSCell.CellState.NOT_DISCOVERED) 
            {
                undiscoveredNeighbours.Add(upperCandidate);
            }

            if (maze.Cells.GetLength(0) - 1 is var lastRowIndex &&
                    cell.Row < lastRowIndex
                    && maze.Cells[cell.Row + 1, cell.Col] is var lowerCandidate 
                    && lowerCandidate.IsFloor
                    && lowerCandidate.State == BFSCell.CellState.NOT_DISCOVERED) 
            {
                undiscoveredNeighbours.Add(lowerCandidate);
            }

            if (cell.Col > 0 
                    && maze.Cells[cell.Row, cell.Col - 1] is var leftCandidate 
                    && leftCandidate.IsFloor
                    && leftCandidate.State == BFSCell.CellState.NOT_DISCOVERED) 
            {
                undiscoveredNeighbours.Add(leftCandidate);
            }

            if (maze.Cells.GetLength(1) - 1 is var lastColIndex 
                    && cell.Col < lastColIndex
                    && maze.Cells[cell.Row, cell.Col + 1] is var rightCandidate 
                    && rightCandidate.IsFloor
                    && rightCandidate.State == BFSCell.CellState.NOT_DISCOVERED) 
            {
                undiscoveredNeighbours.Add(rightCandidate);
            }

            return undiscoveredNeighbours;
        }
    }
}
