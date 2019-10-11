using System;

namespace pacman
{
    class Board
    {
        public static int BOARD_DEMENSION_ROW = 28;
        public static int BOARD_DEMENSION_COLUMN = 30;
        public static int INITIAL_COOKIES = (BOARD_DEMENSION_ROW - 2) * (BOARD_DEMENSION_COLUMN - 2);

        public char[,] GameBoard { get; set;}
        public PacPerson PacPerson { get; }
        public Ghost GhostOne { get; }
        public PacPersonLocation CurrentPacPersonLocation { get; set; }
        public PacPersonLocation CurrentGhostOneLocation { get; set; }
        public int Cookies { get; set; }

        public Board(PacPerson pacPerson, Ghost ghostOne)
        {
            GameBoard = CreateBoard();
            PacPerson = pacPerson;
            GhostOne = ghostOne;
            CurrentPacPersonLocation = new PacPersonLocation(BOARD_DEMENSION_ROW - 2, 1);
            CurrentGhostOneLocation = new PacPersonLocation(1, BOARD_DEMENSION_COLUMN - 2);
            Cookies = INITIAL_COOKIES;
        }

        public void RenderBoard()
        {
            Console.Clear();
            AddPacPersonToBoard(CurrentPacPersonLocation, 'O');
            AddPacPersonToBoard(CurrentGhostOneLocation, '\u263A');

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

        public void AddPacPersonToBoard(PacPersonLocation currentLocation, char symbol)
        {
            GameBoard[currentLocation.Row, currentLocation.Column] = symbol;
            Cookies = Cookies - 1; // because pacPerson will 'eat' the cookie at its startig location
        }

        public void UpdatePacPersonLocation()
        {
            int row = CurrentPacPersonLocation.Row;
            int column = CurrentPacPersonLocation.Column;

            switch (PacPerson.CurrentDirection)
            {
                case Direction.Left:
                    if (GameBoard[row, column - 1] != '█' && GameBoard[row, column - 1] != '▄' && GameBoard[row, column - 1] != '▀')
                    {
                        MovePacPersonAndEmptyPreviousLocation(row, column - 1);
                    }
                    break;
                case Direction.Up:
                    if (GameBoard[row - 1, column] != '▄' && GameBoard[row - 1, column] != '▀' && GameBoard[row - 1, column] != '█')
                    {
                        MovePacPersonAndEmptyPreviousLocation(row - 1, column);
                    }
                    break;
                case Direction.Right:
                    if (GameBoard[row, column + 1] != '█' && GameBoard[row, column + 1] != '▄' && GameBoard[row, column + 1] != '▀')
                    {
                        MovePacPersonAndEmptyPreviousLocation(row, column + 1);
                    }
                    break;
                case Direction.Down:
                    if (GameBoard[row + 1, column] != '█' && GameBoard[row + 1, column] != '▄' && GameBoard[row + 1, column] != '▀')
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

        public void MoveGhostLocation(int row, int column)
        {
            char currentBoardState = GameBoard[CurrentGhostOneLocation.Row, CurrentGhostOneLocation.Column];
            if (currentBoardState == '\u263A')
            {
                WriteAt(" ", CurrentGhostOneLocation.Row, CurrentGhostOneLocation.Column);
            }
            else
            {
                WriteAt(currentBoardState.ToString(), CurrentGhostOneLocation.Row, CurrentGhostOneLocation.Column);
            } 
            CurrentGhostOneLocation = new PacPersonLocation(row, column);
            WriteAt("\u263A", CurrentGhostOneLocation.Row, CurrentGhostOneLocation.Column);
        }

        public void UpdateGhostLocation()
        {
            int row = CurrentGhostOneLocation.Row;
            int column = CurrentGhostOneLocation.Column;

            switch (GhostOne.CurrentDirection)
            {
                case Direction.Left:
                    if (GameBoard[row, column - 1] != '█' && GameBoard[row, column - 1] != '▄' && GameBoard[row, column - 1] != '▀')
                    {
                        MoveGhostLocation(row, column - 1);
                    }
                    else
                    {
                        GhostOne.UpdateGhostDirection();
                    }
                    break;
                case Direction.Up:
                    if (GameBoard[row - 1, column] != '▄' && GameBoard[row - 1, column] != '▀' && GameBoard[row - 1, column] != '█')
                    {
                        MoveGhostLocation(row - 1, column);
                    }
                    else
                    {
                        GhostOne.UpdateGhostDirection();
                    }
                    break;
                case Direction.Right:
                    if (GameBoard[row, column + 1] != '█' && GameBoard[row, column + 1] != '▄' && GameBoard[row, column + 1] != '▀')
                    {
                        MoveGhostLocation(row, column + 1);
                    }
                    else
                    {
                        GhostOne.UpdateGhostDirection();
                    }
                    break;
                case Direction.Down:
                    if (GameBoard[row + 1, column] != '█' && GameBoard[row + 1, column] != '▄' && GameBoard[row + 1, column] != '▀')
                    {
                        MoveGhostLocation(row + 1, column);
                    }
                    else
                    {
                        GhostOne.UpdateGhostDirection();
                    }
                    break;
            }
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


        // The ASCII character code for the '█' is alt - 219 and '▀' is alt - 220
        public char[,] CreateBoard()
        {
            return new char[28, 30]
            {
                {'█','▀','▀','▀','▀','▀','▀','▀','▀','▀','▀','▀','▀','█','█','█','█','▀','▀','▀','▀','▀','▀','▀','▀','▀','▀','▀','▀','█'},
                {'█','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','█','█','█','█','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','█'},
                {'█','∙','█','█','█','█','∙','█','█','█','█','█','∙','█','█','█','█','∙','█','█','█','█','█','∙','█','█','█','█','∙','█'},
                {'█','∙','█','█','█','█','∙','█','█','█','█','█','∙','█','█','█','█','∙','█','█','█','█','█','∙','█','█','█','█','∙','█'},
                {'█','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','█'},
                {'█','∙','█','█','█','█','∙','█','█','█','∙','█','█','█','█','█','█','█','█','∙','█','█','█','∙','█','█','█','█','∙','█'},
                {'█','∙','█','█','█','█','∙','█','█','█','∙','∙','∙','█','█','█','█','∙','∙','∙','█','█','█','∙','█','█','█','█','∙','█'},
                {'█','∙','█','█','█','█','∙','█','█','█','█','█','∙','█','█','█','█','∙','█','█','█','█','█','∙','█','█','█','█','∙','█'},
                {'█','∙','∙','∙','∙','∙','∙','█','█','█','█','█','∙','█','█','█','█','∙','█','█','█','█','█','∙','∙','∙','∙','∙','∙','█'},
                {'█','▄','▄','▄','▄','▄','∙','█','█','█','█','█','∙','█','█','█','█','∙','█','█','█','█','█','∙','▄','▄','▄','▄','▄','█'},
                {' ',' ',' ',' ',' ','█','∙','█','█','█','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','█','█','█','∙','█',' ',' ',' ',' ',' '},
                {'▄','▄','▄','▄','▄','█','∙','█','█','█','∙','▄','▄','▄','▄','▄','▄','▄','▄','∙','█','█','█','∙','█','▄','▄','▄','▄','▄'},
                {'∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','█','█','█','█','█','█','█','█','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙'},
                {'▄','▄','▄','▄','▄','▄','∙','▄','▄','▄','▄','█','█','█','█','█','█','█','█','▄','▄','▄','▄','∙','▄','▄','▄','▄','▄','▄'},
                {' ',' ',' ',' ',' ','█','∙','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','█','∙','█',' ',' ',' ',' ',' '},
                {'█','▀','▀','▀','▀','▀','∙','▀','▀','▀','▀','▀','▀','▀','▀','▀','▀','▀','▀','▀','▀','▀','▀','∙','▀','▀','▀','▀','▀','█'},
                {'█','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','█'},
                {'█','∙','█','█','█','█','█','█','∙','∙','∙','█','█','∙','∙','∙','∙','∙','∙','█','█','█','█','∙','∙','∙','∙','∙','∙','█'},
                {'█','∙','█','█','█','█','█','█','∙','∙','∙','█','█','∙','∙','∙','∙','∙','∙','█','█','█','█','█','∙','∙','∙','∙','∙','█'},
                {'█','∙','∙','∙','█','█','∙','∙','∙','∙','∙','█','█','∙','∙','∙','∙','∙','∙','█','∙','∙','∙','█','∙','∙','∙','∙','∙','█'},
                {'█','∙','∙','∙','█','█','∙','∙','∙','∙','∙','█','█','∙','∙','∙','∙','∙','∙','█','∙','∙','∙','∙','∙','∙','∙','∙','∙','█'},
                {'█','∙','∙','∙','█','█','∙','∙','∙','∙','∙','█','█','∙','∙','∙','∙','∙','∙','█','∙','∙','∙','∙','∙','∙','∙','∙','∙','█'},
                {'█','∙','∙','∙','█','█','∙','∙','∙','∙','∙','█','█','∙','∙','∙','∙','∙','∙','█','∙','∙','█','█','█','█','∙','∙','∙','█'},
                {'█','∙','∙','∙','█','█','∙','∙','∙','∙','∙','█','█','∙','∙','∙','∙','∙','∙','█','∙','∙','∙','∙','∙','█','∙','∙','∙','█'},
                {'█','∙','∙','∙','█','█','∙','∙','∙','∙','∙','█','█','█','█','█','∙','∙','∙','█','∙','∙','∙','█','∙','█','∙','∙','∙','█'},
                {'█','∙','∙','∙','█','█','∙','∙','∙','∙','∙','█','█','█','█','█','∙','∙','∙','█','█','█','█','█','∙','∙','∙','∙','∙','█'},
                {'█','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','∙','█'},
                {'█','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','▄','█'},

            };
        }
    }
}
