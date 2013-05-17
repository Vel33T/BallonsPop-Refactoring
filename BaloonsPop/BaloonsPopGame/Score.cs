namespace BaloonsPopGame
{
    using System;
    using System.IO;

    public class Score : IComparable<Score>
    {
        public string Name { get; private set; }
        public int Points { get; private set; }
        private string[,] chart = new string[5, 2];

        public Score(int points, string name)
        {
            this.Points = points;
            this.Name = name;
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


        private static void SavePlayerPoints(string[,] chart, int points, int i, string difficulty)
        {
            Console.WriteLine("Please, insert your name:");
            string userName = Console.ReadLine();
            chart[i, 0] = points.ToString();
            chart[i, 1] = userName;
        }

        public static bool SignIfSkilled(string[,] chart, int points, string difficulty)
        {
            bool skilled = false;
            int worstMoves = 0;
            int worstMovesChartPosition = 0;
            for (int position = 1; position <= 5; position++)
            {
                if (chart[position, 0] == null)
                {
                    SavePlayerPoints(chart, points, position, difficulty);
                    skilled = true;
                    break;
                }
            }
            if (skilled == false)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (int.Parse(chart[i, 0]) > worstMoves)
                    {
                        worstMovesChartPosition = i;
                        worstMoves = int.Parse(chart[i, 0]);
                    }
                }
            }
            if (points < worstMoves && skilled == false)
            {
                SavePlayerPoints(chart, points, worstMovesChartPosition, difficulty);
                skilled = true;
            }
            return skilled;
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