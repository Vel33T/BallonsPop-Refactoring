namespace BaloonsPopGame
{
    using System;
    
    /// <summary>
    /// Keeps information about player, his name and score achieved
    /// </summary>
    public class Player
    {
        public string Name { get; set; }
        public int Points { get; set; }

        public Player(string name, int points)
        {
            this.Name = name;
            this.Points = points;
        }
    }
}
