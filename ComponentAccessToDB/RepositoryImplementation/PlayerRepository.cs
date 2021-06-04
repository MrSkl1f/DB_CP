using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentAccessToDB
{
    public class PlayerRepository : IPlayerRepository, IDisposable
    {
        private readonly transfersystemContext db;
        private readonly ILogger<PlayerRepository> _logger;
        public PlayerRepository(transfersystemContext curDb, ILogger<PlayerRepository> logger)
        {
            db = curDb;
            _logger = logger;
        }
        public void Add(Player element)
        {
            try
            {
                element.Playerid = db.Players.Count() + 1;
                db.Players.Add(element);
                db.SaveChanges();
                _logger.LogInformation("Player {Name} added at {dateTime}", element.Name, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        public List<Player> GetAll()
        {
            IQueryable<Player> players = db.Players;
            return players.Count() > 0 ? players.ToList() : null;
        }
        public void Update(Player element)
        {
            try
            {
                db.Players.Update(element);
                db.SaveChanges();
                _logger.LogInformation("Player {Name} updated at {dateTime}", element.Name, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        public void Delete(Player element)
        {
            try
            {
                db.Players.Remove(element);
                db.SaveChanges();
                _logger.LogInformation("Player {Name} removed at {dateTime}", element.Name, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        public List<Player> GetPlayersByTeam(Team element)
        {
            IQueryable<Player> players = db.Players.Where(needed => needed.Team.Name == element.Name);
            return players.Count() > 0 ? players.ToList() : null;
        }
        public Player FindPlayerByID(int id)
        {
            return db.Players.Find(id);
        }
        public Player FindPlayerByName(string name)
        {
            IQueryable<Player> players = db.Players.Where(needed => needed.Name == name);
            return players.Count() > 0 ? players.First() : null;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
