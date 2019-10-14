using System;

namespace pacman
{
    class PacPersonLocation
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public PacPersonLocation(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
