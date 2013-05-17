namespace BaloonsPopGame
{
    using System;
    using System.Collections.Generic;

    public class Score
    {
        public string[,] topFive;
        private List<Player> players = new List<Player>();

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
            players.Add(new Player(this.topFive[i, 1], int.Parse(this.topFive[i, 0])));
            players.Sort((x , y) => x.Points.CompareTo(y.Points));
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
            for (int i = 0; i < players.Count; ++i)
            {
                Console.WriteLine("{0}.{1} - {2}", i + 1, players[i].Name, players[i].Points);
            }
            Console.WriteLine("-----------------------------------");


        }
    }
}