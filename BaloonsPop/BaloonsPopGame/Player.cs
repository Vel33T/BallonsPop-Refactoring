using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaloonsPopGame
{
    public class Player
    {
        private string name;
        private int points;
        public Player(string name, int points)
        {
            this.Name = name;
            this.Points = points;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public int Points
        {
            get
            {
                return this.points;
            }
            set
            {
                this.points = value;
            }
        }

        //public int CompareTo(Player other)
        //{
        //    if (this.points > other.points)
        //    {
        //        return 1;
        //    }
        //    else if (this.points < other.points)
        //    {
        //        return -1;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
    }
}
