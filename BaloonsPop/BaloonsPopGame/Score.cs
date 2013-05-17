namespace BaloonsPopGame
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Score : IComparable<Score>
    {
        public string[,] topFive;

        public Score()
        {
            this.topFive = new string[5, 2];
        }

        //private static void SavePlayerPoints(string[,] chart, int points, int i, string difficulty)
        //{
        //    Console.WriteLine("Please, insert your name:");
        //    string userName = Console.ReadLine();
        //    chart[i, 0] = points.ToString();
        //    chart[i, 1] = userName;
        //    using (StreamWriter sw = new StreamWriter("../../results/topFive" + difficulty + ".txt"))
        //    {
        //        for (int j = 0; j < chart.Length / 2; j++)
        //        {
        //            if (chart[j, 1] == null)
        //            {
        //                break;
        //            }
        //            sw.Write("%" + chart[j, 1] + "-" + chart[j, 0]);
        //        }
        //    }
        //}


        private void SavePlayerPoints(int points, int i)
        {
            Console.WriteLine("Please, insert your name:");
            string userName = Console.ReadLine();
            this.topFive[i, 0] = points.ToString();
            this.topFive[i, 1] = userName;
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
            List<Score> scores = new List<Score>();

            for (int i = 0; i < 5; ++i)
            {
                if (this.topFive[i, 0] == null)
                {
                    break;
                }

                scores.Add(int.Parse(this.topFive[i, 0]), this.topFive[i, 1]));
            }

            scores.Sort();

            Console.WriteLine("---------TOP FIVE SCORES-----------");
            for (int i = 0; i < scores.Count; ++i)
            {
                Console.WriteLine("{0}.   {1}", i + 1, scores[i]);
            }
            Console.WriteLine("-----------------------------------");


        }

        public int CompareTo(Score other)
        {
            return Points.CompareTo(other.Points);
        }

        public override string ToString()
        {
            return String.Format("{0} with {1} moves.", this.Name, this.Points);
        }
    }
}