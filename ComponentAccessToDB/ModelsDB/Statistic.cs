using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class Statistic
    {
        public Statistic()
        {
            Players = new HashSet<Player>();
        }

        public int Statisticsid { get; set; }
        public int Numberofwashers { get; set; }
        public int Averagegametime { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
