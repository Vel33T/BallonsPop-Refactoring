namespace BaloonsPopGame
{
    using System;
    using System.Collections.Generic;

    public class Score
    {
        public string[,] topFive;
        private List<Player> scores = new List<Player>();

        public Score()
        {
            this.topFive = new string[5, 2];
        }

        private void SavePlayerPoints(int points, int i)
        {
            Console.WriteLine("Please, insert your name:");
            string userName = Console.ReadLine();
            this.topFive[i, 0] = points.ToString();
            this.topFive[i, 1] = userName;
            scores.Add(new Player(this.topFive[i, 1], int.Parse(this.topFive[i, 0])));
            scores.Sort();
        }

        public bool SignIfSkilled(int points)
        {
            bool skilled = false;
            int worstMoves = 0;
            int worstMovesChartPosition = 0;
            for (int position = 1; position <= 5; position++)
            {
                if (this.topFive[position, 0] == null)
                {
                    SavePlayerPoints(points, position);
                    skilled = true;
                    break;
                }
            }
            if (skilled == false)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (int.Parse(this.topFive[i, 0]) > worstMoves)
                    {
                        worstMovesChartPosition = i;
                        worstMoves = int.Parse(this.topFive[i, 0]);
                    }
                }
            }
            if (points < worstMoves && skilled == false)
            {
                SavePlayerPoints(points, worstMovesChartPosition);
                skilled = true;
            }
            return skilled;
        }

        public void PrintScoreBoard()
        {
            Console.WriteLine("---------TOP FIVE SCORES-----------");
            for (int i = 0; i < scores.Count; ++i)
            {
                Console.WriteLine("{0}.   {1}", i + 1, scores[i].Name, scores[i].Points);
            }
            Console.WriteLine("-----------------------------------");


        }

        public override string ToString()
        {
            return String.Format("{0} with {1} moves.");
        }
    }
}