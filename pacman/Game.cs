using System;
namespace pacman
{
    public class Game
    {
        ConsoleKeyInfo userInput;
        public int cookies = 10;

        //public Game()
        //{
        //    Board = new Board();
        //}

        public static void PlayGame()
        {
            Game currentGame = new Game();
            PacPerson currentPacPerson = new PacPerson();
            while (currentGame.cookies > 0)
            {
                currentGame.GetUserInput(currentPacPerson);
                currentGame.MovePacPerson(currentPacPerson);
                currentGame.cookies--;
                Console.WriteLine(currentGame.cookies);
            }
        }

        private void GetUserInput(PacPerson pacperson)
        {
            do
            {
                Console.WriteLine("Use the arrow keys to choose the direction you would like to move PacMan");
                userInput = Console.ReadKey(true);
                pacperson.ChangeDirection((int)userInput.Key);
            }
                while ((int)userInput.Key < 37 || (int)userInput.Key > 40);
        }

        private void MovePacPerson(PacPerson pacperson)
        {
            if (pacperson.currentDirection == Direction.RightArrow)
            {
                //currentGame.UpdateBoard(0 column++);
            }
            else if (pacperson.currentDirection == Direction.LeftArrow)
            {
                //currentGame.UpdateBoard(0, column--);
            }
            else if (pacperson.currentDirection == Direction.UpArrow)
            {
                //currentGame.UpdateBoard(row--, 0);
            }
            else
            {
                //currentGame.UpdateBoard(row++, 0);
            }
        }
    }
}
