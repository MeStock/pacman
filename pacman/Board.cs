using System;

namespace pacman
{
    class Board
    {
        public static int BOARD_DEMENSION_ROW = 28;
        public static int BOARD_DEMENSION_COLUMN = 30;
        public static int INITIAL_COOKIES = 378;

        public char[,] GameBoard { get; set;}
        public PacPerson PacPerson { get; }
        public Ghost GhostOne { get; }
        public Ghost GhostTwo { get; }
        public Ghost GhostThree { get; }
        public PacPersonLocation CurrentPacPersonLocation { get; set; }
        public PacPersonLocation CurrentGhostOneLocation { get; set; }
        public PacPersonLocation CurrentGhostTwoLocation { get; set; }
        public PacPersonLocation CurrentGhostThreeLocation { get; set; }
        public int Cookies { get; set; }

        public Board(PacPerson pacPerson, Ghost ghostOne, Ghost ghostTwo, Ghost ghostThree)
        {
            GameBoard = CreateBoard();
            PacPerson = pacPerson;
            GhostOne = ghostOne;
            GhostOne = ghostTwo;
            GhostOne = ghostThree;
            CurrentPacPersonLocation = new PacPersonLocation(BOARD_DEMENSION_ROW - 2, 1);
            CurrentGhostOneLocation = new PacPersonLocation(1, BOARD_DEMENSION_COLUMN - 2);
            CurrentGhostTwoLocation = new PacPersonLocation(1, 1);
            CurrentGhostThreeLocation = new PacPersonLocation(BOARD_DEMENSION_ROW - 2, BOARD_DEMENSION_COLUMN - 2);
            Cookies = INITIAL_COOKIES;
        }

        public void RenderBoard()
        {
            Console.Clear();
            AddPacPersonToBoard(CurrentPacPersonLocation, 'O');
            AddPacPersonToBoard(CurrentGhostOneLocation, '\u263A');
            AddPacPersonToBoard(CurrentGhostTwoLocation, '\u263A');
            AddPacPersonToBoard(CurrentGhostThreeLocation, '\u263A');

            for (int row = 0; row < BOARD_DEMENSION_ROW; row++)
            {
                for (int column = 0; column < BOARD_DEMENSION_COLUMN; column++)
                {
                    Console.Write(GameBoard[row, column]);
                }
                Console.WriteLine();
            }
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
                    if (CurrentPacPersonLocation.Row == 12 && CurrentPacPersonLocation.Column == 0)
                    {
                        MovePacPersonAndEmptyPreviousLocation(12, Board.BOARD_DEMENSION_COLUMN - 1);
                    }
                    else if (GameBoard[row, column - 1] != '█' && GameBoard[row, column - 1] != '▄' && GameBoard[row, column - 1] != '▀')
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
                    if (CurrentPacPersonLocation.Row == 12 && CurrentPacPersonLocation.Column == Board.BOARD_DEMENSION_COLUMN - 1)
                    {
                        MovePacPersonAndEmptyPreviousLocation(12, 0);
                    }
                    else if (GameBoard[row, column + 1] != '█' && GameBoard[row, column + 1] != '▄' && GameBoard[row, column + 1] != '▀')
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

        public void MoveGhostLocation(PacPersonLocation ghostLocation, int row, int column)
        {
            char currentBoardState = GameBoard[ghostLocation.Row, ghostLocation.Column];
            if (currentBoardState == '\u263A')
            {
                WriteAt(" ", ghostLocation.Row, ghostLocation.Column);
            }
            else
            {
                WriteAt(currentBoardState.ToString(), ghostLocation.Row, ghostLocation.Column);
            }
            ghostLocation.Row = row;
            ghostLocation.Column = column;
            WriteAt("\u263A", ghostLocation.Row, ghostLocation.Column);
        }

        public void UpdateGhostLocation(Ghost ghost, PacPersonLocation ghostLocation)
        {
            int row = ghostLocation.Row;
            int column = ghostLocation.Column;

            switch (ghost.CurrentDirection)
            {
                case Direction.Left:
                    if (ghostLocation.Row == 12 && ghostLocation.Column == 0)
                    {
                        MoveGhostLocation(ghostLocation, 12, Board.BOARD_DEMENSION_COLUMN - 1);
                    }
                    else if (GameBoard[row, column - 1] != '█' && GameBoard[row, column - 1] != '▄' && GameBoard[row, column - 1] != '▀')
                    {
                        MoveGhostLocation(ghostLocation, row, column - 1);
                    }
                    else
                    {
                        ghost.UpdateGhostDirection();
                    }
                    break;
                case Direction.Up:
                    if (GameBoard[row - 1, column] != '▄' && GameBoard[row - 1, column] != '▀' && GameBoard[row - 1, column] != '█')
                    {
                        MoveGhostLocation(ghostLocation, row - 1, column);
                    }
                    else
                    {
                        ghost.UpdateGhostDirection();
                    }
                    break;
                case Direction.Right:
                    if (ghostLocation.Row == 12 && ghostLocation.Column == Board.BOARD_DEMENSION_COLUMN - 1)
                    {
                        MoveGhostLocation(ghostLocation, 12, 0);
                    }
                    else if (GameBoard[row, column + 1] != '█' && GameBoard[row, column + 1] != '▄' && GameBoard[row, column + 1] != '▀')
                    {
                        MoveGhostLocation(ghostLocation, row, column + 1);
                    }
                    else
                    {
                        ghost.UpdateGhostDirection();
                    }
                    break;
                case Direction.Down:
                    if (GameBoard[row + 1, column] != '█' && GameBoard[row + 1, column] != '▄' && GameBoard[row + 1, column] != '▀')
                    {
                        MoveGhostLocation(ghostLocation, row + 1, column);
                    }
                    else
                    {
                        ghost.UpdateGhostDirection();
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
        {// 12, 0 and 12, BOARD_DIMENSTION_COLUMN - 1
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
