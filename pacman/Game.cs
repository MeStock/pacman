using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace pacman
{
    class Game
    {
        public static PacPerson PacPerson = new PacPerson();
        public static Board Board = new Board(PacPerson);

        public static void Start()
        {
            Board.RenderBoard();

            Thread userInput = new Thread(GetUserInput);
            userInput.Start();

            while (Board.Cookies > 0)
            {
                Board.UpdatePacPersonLocation();
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