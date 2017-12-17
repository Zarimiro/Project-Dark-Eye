using UnityEngine;

namespace Assets.Scripts
{
    public class MazeCell
    {
        public bool Visited;
        public GameObject Floor, NorthWall, SoughtWall, EastWall, WestWall, FloorWall;
    }
}
