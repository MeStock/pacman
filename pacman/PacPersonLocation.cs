using System;

namespace pacman
{
    class PacPersonLocation
    {
        public int Row { get; }
        public int Column { get; }

        public PacPersonLocation(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
