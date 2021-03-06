using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class Availabledeal
    {
        public int Id { get; set; }
        public int? Playerid { get; set; }
        public int? Tomanagementid { get; set; }
        public int? Frommanagementid { get; set; }
        public int Cost { get; set; }
        public int Status { get; set; }

        public virtual Management Frommanagement { get; set; }
        public virtual Player Player { get; set; }
        public virtual Management Tomanagement { get; set; }
    }
}
