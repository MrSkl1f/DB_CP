using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class Team
    {
        public Team()
        {
            Desiredplayers = new HashSet<Desiredplayer>();
            Players = new HashSet<Player>();
        }

        public int Teamid { get; set; }
        public int? Managementid { get; set; }
        public string Name { get; set; }
        public string Headcoach { get; set; }
        public string Country { get; set; }
        public string Stadium { get; set; }
        public int Balance { get; set; }

        public virtual Management Management { get; set; }
        public virtual ICollection<Desiredplayer> Desiredplayers { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
