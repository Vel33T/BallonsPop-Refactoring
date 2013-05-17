namespace BaloonsPopGame
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Score
    {
        /// <summary>
        /// List for keeping all players good enough to go to top 5
        /// </summary>
        public List<Player> players { get; private set; }

        /// <summary>
        /// Constructor for the class Score
        /// </summary>
        public Score()
        {
            this.players = new List<Player>();
        }

        /// <summary>
        /// Checks if the player that just finished the game
        /// is good enough to be put in TOP 5 scores
        /// </summary>
        /// <returns>Boolean value</returns>
        public bool IsGoodEnough(int moves)
        {
            if (this.players.Count < 5)
            {
                return true;
            }
            if (this.players[4].Points > moves)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds player to the Scoreboard. If there are no free places
        /// the method removes the fifth player. And adds the new one there.
        /// </summary>
        /// <param name="name">Player's name</param>
        /// <param name="moves">Player's moves</param>
        public void AddPlayer(string name, int moves)
        {
            if (this.players.Count == 5)
            {
                this.players.RemoveAt(4);
                this.players.Add(new Player(name, moves));
            }
            else
            {
                this.players.Add(new Player(name, moves));
            }
        }

        /// <summary>
        /// Sorts the scores. The ones with fewer moves goes on top.
        /// </summary>
        public void Sort()
        {
            players.Sort((x, y) => x.Points.CompareTo(y.Points));
        }

        /// <summary>
        /// Generates string containing the scoreboard.
        /// </summary>
        /// <returns>Returns the generated string to be used for rendering</returns>
        public string GetScoreBoard()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("---------TOP FIVE SCORES-----------\n");
            for (int i = 0; i < players.Count; ++i)
            {
                sb.AppendFormat("{0}.{1} - {2}\n", i + 1, players[i].Name, players[i].Points);
            }
            sb.Append("-----------------------------------");
            return sb.ToString();
        }
    }
}