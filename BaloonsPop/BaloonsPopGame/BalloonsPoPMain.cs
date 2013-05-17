﻿namespace BaloonsPopGame
{
    using System;
    using System.Text;

    public class BalloonsPopMain
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(PrintWelcomeMessage());

            string difficulty = Console.ReadLine().ToLower();
            GameEngine game = new GameEngine(difficulty);

            Console.WriteLine(game.GetMatrixImage());

            Console.WriteLine("Enter a row and column: ");
            string userInput = Console.ReadLine();
            while (true)
            {
                userInput = userInput.ToUpper().Trim();
                if (userInput == "EXIT")
                {
                    Console.WriteLine("Game over! Have a nice day!");
                    return;
                }
                try
                {
                    game.ProcessGame(userInput);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("Enter a row and column: ");
                userInput = Console.ReadLine();
            }
        }

        public static string PrintWelcomeMessage()
        {
            StringBuilder welcomeMessage = new StringBuilder();

             welcomeMessage.AppendLine("********************************");
             welcomeMessage.AppendLine("* Welcome to Balloons Pop Game *");
             welcomeMessage.AppendLine("********************************");
             welcomeMessage.AppendLine("");
             welcomeMessage.AppendLine("Please, insert \"TOP\" to see Top Five score board.");
             welcomeMessage.AppendLine("Please, insert \"RESTART\" to exit the game.");
             welcomeMessage.AppendLine("Please, insert \"EXIT\" to exit the game.");
             welcomeMessage.AppendLine("");
             welcomeMessage.AppendLine("Please, insert what difficulty do you want? - Easy, Medium, Hard");

             return welcomeMessage.ToString();
        }
    }
}
