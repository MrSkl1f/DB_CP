using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentAccessToDB
{
    public interface IDesiredPlayersRepository : CrudRepository<Desiredplayer>
    {
        Desiredplayer GetPlayerByID(int id);
        List<Desiredplayer> GetPlayersByManagement(Management element);
    }
}
