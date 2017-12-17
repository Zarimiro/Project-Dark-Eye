﻿namespace Assets.Scripts
{
    public abstract class MazeAlgoritm
    {
        protected int Rows, Columns;
        protected MazeCell[,] MazeCells;

        protected MazeAlgoritm(MazeCell[,] mazecells)
        {
            MazeCells = mazecells;
            Rows = MazeCells.GetLength(0);
            Columns = MazeCells.GetLength(1);
        }

        public abstract void CreateMaze();
    }
}