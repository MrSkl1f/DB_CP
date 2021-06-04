using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class Player
    {
        public Player()
        {
            Availabledeals = new HashSet<Availabledeal>();
            Desiredplayers = new HashSet<Desiredplayer>();
        }

        public int Playerid { get; set; }
        public int? Teamid { get; set; }
        public int? Statistics { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Number { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public int Cost { get; set; }

        public virtual Statistic StatisticsNavigation { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<Availabledeal> Availabledeals { get; set; }
        public virtual ICollection<Desiredplayer> Desiredplayers { get; set; }
    }
}
