using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class Desiredplayer
    {
        public int Id { get; set; }
        public int? Playerid { get; set; }
        public int? Teamid { get; set; }
        public int? Managementid { get; set; }

        public virtual Management Management { get; set; }
        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
}
