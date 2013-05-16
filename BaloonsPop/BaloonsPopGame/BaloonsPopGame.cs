namespace BaloonsPopGame
{
    using System;

    public class BaloonsPopGame
    {
        private static string ChooseDifficulty()
        {
            while (true)
            {
                Console.WriteLine("What difficulty you want? - Easy, Medium, Hard");
                string difficulty = Console.ReadLine();
                if (difficulty == "Easy" || difficulty == "easy" || difficulty == "EASY")
                {
                    return "Easy";
                }
                else if (difficulty == "Medium" || difficulty == "medium" || difficulty == "MEDIUM")
                {
                    return "Medium";
                }
                else if (difficulty == "Hard" || difficulty == "hard" || difficulty == "HARD")
                {
                    return "Hard";
                }
                else
                {
                    Console.WriteLine("There is no such difficulty!");
                }
            }
            
        }

        public static void Main(string[] args)
        {
            //Proba
            string difficulty = ChooseDifficulty();
            BaloonsPopGameEngine game = new BaloonsPopGameEngine(difficulty);
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
