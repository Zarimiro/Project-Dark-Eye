using UnityEngine;

namespace Assets.Scripts
{
    public class HuntAndKillAlgoritm : MazeAlgoritm
    {
        private int _currentRow;
        private int _currentColumn;
        private bool _courseVisited;

        public HuntAndKillAlgoritm(MazeCell[,] mazecells) : base(mazecells)
        {
        }

        public override void CreateMaze()
        {
            HuntAndKill();
        }

        private void HuntAndKill()
        {
            MazeCells[_currentRow, _currentColumn].Visited = true;

            while (!_courseVisited)
            {
                Kill();
                Hunt();
            }
        }


        private void Kill()
        {
            while (RouteStillAvailiable(_currentRow, _currentColumn))
            {
               // var direction = Random.Range(1,5);
                var direction = NumberGenerator.GetNextNumber();

                if (direction == 1 && CellAvailiable(_currentRow - 1, _currentColumn))
                {
                    //North
                    DestroyWallIfExists(MazeCells[_currentRow - 1, _currentColumn].SoughtWall);
                    DestroyWallIfExists(MazeCells[_currentRow, _currentColumn].NorthWall);
                    _currentRow--;
                }

                else if (direction == 2 && CellAvailiable(_currentRow + 1, _currentColumn))
                {
                    // South
                    DestroyWallIfExists(MazeCells[_currentRow + 1, _currentColumn].NorthWall);
                    DestroyWallIfExists(MazeCells[_currentRow, _currentColumn].SoughtWall);
                    _currentRow++;
                }
                else if (direction == 3 && CellAvailiable(_currentRow, _currentColumn - 1))
                {
                    //west
                    DestroyWallIfExists(MazeCells[_currentRow, _currentColumn - 1].EastWall);
                    DestroyWallIfExists(MazeCells[_currentRow, _currentColumn].WestWall);
                    _currentColumn--;
                }
                else if (direction == 4 && CellAvailiable(_currentRow, _currentColumn + 1))
                {
                    //east
                    DestroyWallIfExists(MazeCells[_currentRow, _currentColumn + 1].WestWall);
                    DestroyWallIfExists(MazeCells[_currentRow, _currentColumn].EastWall);
                    _currentColumn++;
                }
                MazeCells[_currentRow, _currentColumn].Visited = true;
            }
        }

        private void Hunt()
        {
            _courseVisited = true;

            for (var r = 0; r < Rows; r++)
            {
                for (var c = 0; c < Columns; c++)
                {
                    if (MazeCells[r, c].Visited || !CellAdjustmentAcess(r, c)) continue;

                    _courseVisited = false;
                    _currentColumn = c; _currentRow = r;
                    DestroyAdjacentWall(_currentRow, _currentColumn);
                    MazeCells[r, c].Visited = true;
                    return;
                }
            }

        }

        private bool RouteStillAvailiable(int row, int column)
        {
            var routesAvailiable = 0;

            if (row > 0 && !MazeCells[row - 1, column].Visited)
            {
                routesAvailiable++;
            }
            if (row < Rows - 1 && !MazeCells[row + 1, column].Visited)
            {
                routesAvailiable++;
            }
            if (column > 0 && !MazeCells[row, column - 1].Visited)
            {
                routesAvailiable++;
            }
            if (column < Columns - 1 && !MazeCells[row, column + 1].Visited)
            {
                routesAvailiable++;
            }
            return routesAvailiable > 0;
        }

        private bool CellAvailiable(int row, int column)
        {
            return row >= 0 && row < Rows && column >= 0 && column < Columns && !MazeCells[row, column].Visited;
        }

        private void DestroyWallIfExists(GameObject wall)
        {
            if (wall != null)
            {
                GameObject.DestroyObject(wall);
            }
        }

        private bool CellAdjustmentAcess(int row, int column)
        {
            var visitedCells = 0;

            if (row > 0 && MazeCells[row - 1, column].Visited)
            {
                visitedCells++;
            }
            if (row < Rows - 2 && MazeCells[row + 1, column].Visited)
            {
                visitedCells++;
            }
            if (column > 0 && MazeCells[row, column - 1].Visited)
            {
                visitedCells++;
            }
            if (column < Columns - 2 && MazeCells[row, column + 1].Visited)
            {
                visitedCells++;
            }
            return visitedCells > 0;
        }

        private void DestroyAdjacentWall(int row, int column)
        {
            var wallDestroyed = false;

            while (!wallDestroyed)
            {
                var direction = Random.Range(1, 4);
                // int direction = NumberGenerator.GetNextNumber();
                if (direction == 1 && row > 0 && MazeCells[row - 1, column].Visited)
                {
                    DestroyWallIfExists(MazeCells[row - 1, column].SoughtWall);
                    DestroyWallIfExists(MazeCells[row, column].NorthWall);
                    wallDestroyed = true;
                }

                if (direction == 2 && row < Rows - 2 && MazeCells[row + 1, column].Visited)
                {
                    DestroyWallIfExists(MazeCells[row + 1, column].NorthWall);
                    DestroyWallIfExists(MazeCells[row, column].SoughtWall);
                    wallDestroyed = true;
                }

                if (direction == 3 && column > 0 && MazeCells[row, column - 1].Visited)
                {
                    DestroyWallIfExists(MazeCells[row, column - 1].EastWall);
                    DestroyWallIfExists(MazeCells[row, column].WestWall);
                    wallDestroyed = true;
                }

                if (direction != 4 || column >= Columns - 2 || !MazeCells[row, column + 1].Visited) continue;

                DestroyWallIfExists(MazeCells[row, column + 1].WestWall);
                DestroyWallIfExists(MazeCells[row, column].EastWall);
                wallDestroyed = true;
            }
        }
    }
}