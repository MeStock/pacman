using System;
using System.Threading;

namespace pacman
{
    public class Ghost : PacPerson
    {
        public Ghost()
        {
            CurrentDirection = GetGhostRandomDirection();
        }

        public Direction GetGhostRandomDirection()
        {
            Random rnd = new Random();
            int randomDirection = rnd.Next(37, 41);
            return (Direction)randomDirection;
        }

        public void UpdateGhostDirection()
        {
            CurrentDirection = GetGhostRandomDirection();
        }

        public void ContinouslyUpdateGhostDirection()
        {
            while (true)
            {
                CurrentDirection = GetGhostRandomDirection();
                Thread.Sleep(1000);
            }
        }
    }
}
