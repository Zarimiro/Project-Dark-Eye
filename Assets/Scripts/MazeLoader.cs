using UnityEngine;

namespace Assets.Scripts
{
    public class MazeLoader: MonoBehaviour
    {
        public GameObject Wall;
        public GameObject WallFire;
        public GameObject Floor;
        public GameObject FloorWithDebris;
        public GameObject Parent;
        public int Rows, Columns;
        public float Size = 2f;

        private MazeCell[,] _mazeCells;



        private void Start()
        {
            InitializeCells();
            MazeAlgoritm ma = new HuntAndKillAlgoritm(_mazeCells);
            ma.CreateMaze();        
        }

        public void InitializeCells()
        {
            _mazeCells = new MazeCell[Rows, Columns];

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    _mazeCells[r, c] = new MazeCell();

                    int chance1 = Random.Range(0, 19);
                    _mazeCells[r, c].Floor = Instantiate(chance1 == 4 ? FloorWithDebris : Floor, new Vector3((r * Size), 0, (c * Size) - 3f)
                        , Quaternion.identity);
                    _mazeCells[r, c].Floor.transform.parent = Parent.transform;
                    _mazeCells[r, c].Floor.name = "Floor " + r + " " + c;
                    _mazeCells[r, c].Floor.transform.Rotate(Vector3.right, 90f);


                    if (r == 0)
                    {
                        _mazeCells[r, c].NorthWall = Instantiate(Wall, new Vector3(r * Size - (Size / 2f), 0, c * Size), Quaternion.identity);
                        _mazeCells[r, c].NorthWall.transform.parent = Parent.transform;
                        _mazeCells[r, c].NorthWall.name = "NorthWall" + r + "," + c;
                        _mazeCells[r, c].NorthWall.transform.Rotate(Vector3.up * 90f);
                    }

                    _mazeCells[r, c].SoughtWall = Instantiate(chance1 == 3 ? WallFire : Wall, new Vector3(r * Size + (Size / 2f), 0, c * Size),
                        Quaternion.identity);
                    _mazeCells[r, c].SoughtWall.transform.parent = Parent.transform;
                    _mazeCells[r, c].SoughtWall.name = "SouthWall " + r + "," + c;
                    _mazeCells[r, c].SoughtWall.transform.Rotate(Vector3.up * 90f);

                    if (c == 0)
                    {
                        _mazeCells[r, c].WestWall = Instantiate(Wall, new Vector3(r * Size, 0, c * Size - (Size / 2f)), Quaternion.identity);
                        _mazeCells[r, c].WestWall.transform.parent = Parent.transform;
                        _mazeCells[r, c].WestWall.name = "Westwall" + r + "," + c;
                    }

                
                    _mazeCells[r, c].EastWall = Instantiate(Wall, new Vector3(r * Size, 0, c * Size + (Size / 2f)), Quaternion.identity);
                    _mazeCells[r, c].EastWall.transform.parent = Parent.transform;
                    _mazeCells[r, c].EastWall.name = "EastWall" + r + "," + c;

                }

            
            }
        }
    }
}

