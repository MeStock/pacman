using System;

namespace pacman
{
    class Board
    {   // This is a variable that belongs to the class 
        public static int BOARD_DEMENSIONS_ROW = 25;
        public static int BOARD_DEMENSIONS_COLUMN = 50;
        public static int INITIAL_COOKIES = 63;
        //These are variables that belong to the board
        public char[,] GameBoard { get; }
        public PacPersonLocation CurrentPacmanLocation { get; set; }
        public int Cookies { get; set; }

        public Board()
        {
            GameBoard = new char[BOARD_DEMENSIONS_ROW, BOARD_DEMENSIONS_COLUMN];
            this.AddBoardEdges();
            CurrentPacmanLocation = new PacPersonLocation(8, 1);
            Cookies = INITIAL_COOKIES;
        }

        public void AddBoardEdges()
        {
            for (int row = 0; row < BOARD_DEMENSIONS_ROW; row++)
            {
                for (int column = 0; column < BOARD_DEMENSIONS_COLUMN; column++)
                {
                    if (row == 0 || row == BOARD_DEMENSIONS_ROW - 1)
                    {
                        GameBoard[row, column] = '—';
                    }
                    else
                    {
                        GameBoard[row, 0] = '|';
                        GameBoard[row, BOARD_DEMENSIONS_COLUMN - 1] = '|';
                    }
                }
            }
            GameBoard[0, 0] = '+';
            GameBoard[0, BOARD_DEMENSIONS_COLUMN - 1] = '+';
            GameBoard[BOARD_DEMENSIONS_ROW - 1, 0] = '+';
            GameBoard[BOARD_DEMENSIONS_ROW - 1, BOARD_DEMENSIONS_COLUMN - 1] = '+';
        }

        public void RenderBoard()
        {
            for (int row = 0; row < BOARD_DEMENSIONS_ROW; row++)
            {
                for (int column = 0; column < BOARD_DEMENSIONS_COLUMN; column++)
                {
                    Console.Write(GameBoard[row, column]);
                }
                Console.WriteLine();
            }
        }
    }
}
