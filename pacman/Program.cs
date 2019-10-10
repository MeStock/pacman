using System;

namespace pacman
{
    class PacmanLocation
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public PacmanLocation(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
