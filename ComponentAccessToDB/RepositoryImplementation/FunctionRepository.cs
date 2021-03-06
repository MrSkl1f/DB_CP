using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentAccessToDB
{
    public class FunctionRepository : IFunctionsRepository, IDisposable
    {
        private readonly transfersystemContext db;
        private readonly ILogger<FunctionRepository> _logger;
        public FunctionRepository(transfersystemContext curDb, ILogger<FunctionRepository> logger)
        {
            db = curDb;
            _logger = logger;
        }
        public List<PlayersTeamStat> GetPlayersTeamStat()
        {
            IQueryable<PlayersTeamStat> players = db.getplayers();
            return players.Count() > 0 ? players.ToList() : null;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
