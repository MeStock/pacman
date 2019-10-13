using System;
using System.Threading;

namespace pacman
{
    class Game
    {
        public static PacPerson PacPerson = new PacPerson();
        public static Ghost GhostOne = new Ghost();
        public static Ghost GhostTwo = new Ghost();
        public static Ghost GhostThree = new Ghost();
        public static Board Board = new Board(PacPerson, GhostOne, GhostTwo, GhostThree);

        public static void Start()
        {
            Board.RenderBoard();

            Thread userInput = new Thread(GetUserInput);
            Thread controlGhostOneMovements = new Thread(GhostOne.ContinouslyUpdateGhostDirection);
            Thread controlGhostTwoMovements = new Thread(GhostTwo.ContinouslyUpdateGhostDirection);
            Thread controlGhostThreeMovements = new Thread(GhostThree.ContinouslyUpdateGhostDirection);
            userInput.Start();
            controlGhostOneMovements.Start();
            controlGhostTwoMovements.Start();
            controlGhostThreeMovements.Start();

            while (true)
            {
                Board.UpdatePacPersonLocation();
                Board.UpdateGhostLocation(GhostOne, Board.CurrentGhostOneLocation);
                Board.UpdateGhostLocation(GhostTwo, Board.CurrentGhostTwoLocation);
                Board.UpdateGhostLocation(GhostThree, Board.CurrentGhostThreeLocation);
                if ((Board.CurrentPacPersonLocation.Row == Board.CurrentGhostOneLocation.Row &&
                    Board.CurrentPacPersonLocation.Column == Board.CurrentGhostOneLocation.Column) ||

                    (Board.CurrentPacPersonLocation.Row == Board.CurrentGhostTwoLocation.Row &&
                    Board.CurrentPacPersonLocation.Column == Board.CurrentGhostTwoLocation.Column) ||

                    (Board.CurrentPacPersonLocation.Row == Board.CurrentGhostThreeLocation.Row &&
                    Board.CurrentPacPersonLocation.Column == Board.CurrentGhostThreeLocation.Column) ||

                    Board.Cookies == 0)
                {
                    Environment.Exit(0);
                }
                Thread.Sleep(100);
            }
        }

        private static void GetUserInput()
        {
            ConsoleKeyInfo userInput;
            while (Board.Cookies > 0)
            {
                userInput = Console.ReadKey();
                if (userInput.Key == ConsoleKey.UpArrow)
                {
                    PacPerson.ChangePacPersonDirection((Direction)userInput.Key);
                }
                else if (userInput.Key == ConsoleKey.RightArrow)
                {
                    PacPerson.ChangePacPersonDirection((Direction)userInput.Key);
                }
                else if (userInput.Key == ConsoleKey.DownArrow)
                {
                    PacPerson.ChangePacPersonDirection((Direction)userInput.Key);
                }
                else if (userInput.Key == ConsoleKey.LeftArrow)
                {
                    PacPerson.ChangePacPersonDirection((Direction)userInput.Key);
                }
            }
        }
    }
}