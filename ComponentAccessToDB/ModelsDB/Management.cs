using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class Management
    {
        public Management()
        {
            AvailabledealFrommanagements = new HashSet<Availabledeal>();
            AvailabledealTomanagements = new HashSet<Availabledeal>();
            Desiredplayers = new HashSet<Desiredplayer>();
            Teams = new HashSet<Team>();
        }

        public int Managementid { get; set; }
        public int? Analysistid { get; set; }
        public int? Managerid { get; set; }

        public virtual Userinfo Analysist { get; set; }
        public virtual Userinfo Manager { get; set; }
        public virtual ICollection<Availabledeal> AvailabledealFrommanagements { get; set; }
        public virtual ICollection<Availabledeal> AvailabledealTomanagements { get; set; }
        public virtual ICollection<Desiredplayer> Desiredplayers { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
