using System;
using System.Threading;

namespace pacman
{
    class Game
    {
        public static PacPerson PacPerson = new PacPerson();
        public static Ghost GhostOne = new Ghost();
        public static Board Board = new Board(PacPerson, GhostOne);

        public static void Start()
        {
            Board.RenderBoard();

            Thread userInput = new Thread(GetUserInput);
            Thread controlGhostMovements = new Thread(GhostOne.ContinouslyUpdateGhostDirection);
            userInput.Start();
            controlGhostMovements.Start();

            while (Board.Cookies > 0)
            {
                Board.UpdatePacPersonLocation();
                Board.UpdateGhostLocation();
                if (Board.CurrentPacPersonLocation.Row == Board.CurrentGhostOneLocation.Row &&
                    Board.CurrentPacPersonLocation.Column == Board.CurrentGhostOneLocation.Column)
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