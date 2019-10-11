  
using System;

namespace pacman
{
    public enum Direction { Left = 37, Up = 38, Right = 39, Down = 40 };

    class PacPerson
    {
        public Direction CurrentDirection { get; set; }

        public PacPerson()
        {
            CurrentDirection = Direction.Right;
        }

        public void ChangePacPersonDirection(Direction directionToFace)
        {
            CurrentDirection = directionToFace;
        }
    }
}
