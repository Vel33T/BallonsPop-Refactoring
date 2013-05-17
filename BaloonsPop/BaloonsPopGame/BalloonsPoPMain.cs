namespace BaloonsPopGame
{
    using System;

    public class BalloonsPopMain
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("What difficulty you want? - Easy, Medium, Hard");
            string difficulty = Console.ReadLine().ToLower();
            GameEngine game = new GameEngine(difficulty);
            Console.WriteLine(game.GetMatrixImage());
            Console.WriteLine("Enter a row and column: ");
            string userInput = Console.ReadLine();
            do
            {
                userInput = userInput.ToUpper().Trim();
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
            while (userInput != "EXIT");

            Console.WriteLine("Good Bye!");
        }
    }
}
