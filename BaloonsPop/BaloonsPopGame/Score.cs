namespace BaloonsPopGame
{
    using System;
    using System.Collections.Generic;

    public class Score
    {
        public List<Player> players { get; private set; }

        public Score()
        {
            this.players = new List<Player>();
        }

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

        public void Sort()
        {
            players.Sort((x, y) => x.Points.CompareTo(y.Points));
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