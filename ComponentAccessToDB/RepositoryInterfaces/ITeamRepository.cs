using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentAccessToDB
{
    public interface ITeamRepository : CrudRepository<Team>
    {
        Team FindTeamByID(int id);
        Team FindTeamByName(string name);
        Team FindTeamByPlayer(Player player);
        Team FindTeamByManagement(Management management);
    }
}
