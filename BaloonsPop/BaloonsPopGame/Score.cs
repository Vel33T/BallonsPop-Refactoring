namespace BaloonsPopGame
{
    using System;

    public class Score : IComparable<Score>
    {
        public string Name { get; private set; }
        public int Points { get; private set; }

        public Score(int points, string name)
        {
            this.Points = points;
            this.Name = name;
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