using System;

namespace pacman
{
    class Board
    {
        public static int BOARD_DEMENSION_ROW = 20;
        public static int BOARD_DEMENSION_COLUMN = BOARD_DEMENSION_ROW * 2;
        public static int INITIAL_COOKIES = (BOARD_DEMENSION_ROW - 2) * (BOARD_DEMENSION_COLUMN - 2);

        public char[,] GameBoard { get; }
        public PacPerson PacPerson { get; }
        public PacPersonLocation CurrentPacPersonLocation { get; set; }
        public int Cookies { get; set; }

        public Board(PacPerson pacPerson)
        {
            GameBoard = new char[BOARD_DEMENSION_ROW, BOARD_DEMENSION_COLUMN];
            PacPerson = pacPerson;
            CurrentPacPersonLocation = new PacPersonLocation(BOARD_DEMENSION_ROW - 2, 1);
            Cookies = INITIAL_COOKIES;
        }

        public void RenderBoard()
        {
            Console.Clear();
            FillBoard();
            AddPacPersonToBoard();

            for (int row = 0; row < BOARD_DEMENSION_ROW; row++)
            {
                for (int column = 0; column < BOARD_DEMENSION_COLUMN; column++)
                {
                    Console.Write(GameBoard[row, column]);
                }
                Console.WriteLine();
            }
        }

        public void FillBoard()
        {
            for (int row = 0; row < BOARD_DEMENSION_ROW; row++)
            {
                for (int column = 0; column < BOARD_DEMENSION_COLUMN; column++)
                {
                    if (row == 0 || row == BOARD_DEMENSION_ROW - 1)
                    {
                        GameBoard[row, column] = '=';
                    }
                    else
                    {
                        GameBoard[row, column] = '∙';
                    }
                }

                GameBoard[row, 0] = '|';
                GameBoard[row, BOARD_DEMENSION_COLUMN - 1] = '|';
            }

            GameBoard[0, 0] = '+';
            GameBoard[0, BOARD_DEMENSION_COLUMN - 1] = '+';
            GameBoard[BOARD_DEMENSION_ROW - 1, 0] = '+';
            GameBoard[BOARD_DEMENSION_ROW - 1, BOARD_DEMENSION_COLUMN - 1] = '+';
        }

        public void AddPacPersonToBoard()
        {
            GameBoard[CurrentPacPersonLocation.Row, CurrentPacPersonLocation.Column] = 'O';
            Cookies = Cookies - 1; // because pacPerson will 'eat' the cookie at its startig location
        }

        public void UpdatePacPersonLocation()
        {
            int row = CurrentPacPersonLocation.Row;
            int column = CurrentPacPersonLocation.Column;

            switch (PacPerson.CurrentDirection)
            {
                case Direction.Left:
                    if (GameBoard[row, column - 1] != '|')
                    {
                        MovePacPersonAndEmptyPreviousLocation(row, column - 1);
                    }
                    break;
                case Direction.Up:
                    if (GameBoard[row - 1, column] != '=')
                    {
                        MovePacPersonAndEmptyPreviousLocation(row - 1, column);
                    }
                    break;
                case Direction.Right:
                    if (GameBoard[row, column + 1] != '|')
                    {
                        MovePacPersonAndEmptyPreviousLocation(row, column + 1);
                    }
                    break;
                case Direction.Down:
                    if (GameBoard[row + 1, column] != '=')
                    {
                        MovePacPersonAndEmptyPreviousLocation(row + 1, column);
                    }
                    break;
            }
        }

        public void MovePacPersonAndEmptyPreviousLocation(int row, int column)
        {
            WriteAt(" ", CurrentPacPersonLocation.Row, CurrentPacPersonLocation.Column);
            GameBoard[CurrentPacPersonLocation.Row, CurrentPacPersonLocation.Column] = ' ';
            CurrentPacPersonLocation = new PacPersonLocation(row, column);
            DecrementCookiesIfNeeded(CurrentPacPersonLocation.Row, CurrentPacPersonLocation.Column);
            WriteAt("O", CurrentPacPersonLocation.Row, CurrentPacPersonLocation.Column);
        }

        private static void WriteAt(string s, int row, int column)
        {
            try
            {
                Console.SetCursorPosition(column, row);
                Console.Write(s);

                // moves cursor outside of GameBoard
                Console.SetCursorPosition(BOARD_DEMENSION_COLUMN + 1, BOARD_DEMENSION_ROW + 1);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public void DecrementCookiesIfNeeded(int row, int column)
        {
            if (GameBoard[row, column] == '∙')
            {
                Cookies = Cookies - 1;
            }
        }
    }
}
