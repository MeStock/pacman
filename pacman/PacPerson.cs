using System;
namespace pacman
{
    public enum Direction { LeftArrow = 37, UpArrow = 38, RightArrow = 39, DownArrow = 40};

public class PacPerson
    {
        public Direction currentDirection { get; set; }

        public PacPerson()
        {
            currentDirection = Direction.RightArrow;
        }

        public void ChangeDirection(int userInput)
        {
            switch (userInput)
            {
                case (int)Direction.LeftArrow:
                    currentDirection = Direction.LeftArrow;
                    break;
                case (int)Direction.UpArrow:
                    currentDirection = Direction.UpArrow;
                    break;
                case (int)Direction.DownArrow:
                    currentDirection = Direction.DownArrow;
                    break;
                case (int)Direction.RightArrow:
                    currentDirection = Direction.RightArrow;
                    break;
                default: 
                    break;
            }
        }
    }
}
