using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentAccessToDB
{
    public interface IPlayerRepository : CrudRepository<Player>
    {
        List<Player> GetPlayersByTeam(Team element);
        Player FindPlayerByID(int id);
        Player FindPlayerByName(string name); 
    }
}
