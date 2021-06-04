using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentAccessToDB
{
    public class DesiredPlayersRepository : IDesiredPlayersRepository, IDisposable
    {
        private readonly transfersystemContext db;
        private readonly ILogger<DesiredPlayersRepository> _logger;
        public DesiredPlayersRepository(transfersystemContext curDb, ILogger<DesiredPlayersRepository> logger)
        {
            db = curDb;
            _logger = logger;
        }
        public void Add(Desiredplayer element)
        {
            try
            {
                element.Id = db.Desiredplayers.Count() + 1;
                db.Desiredplayers.Add(element);
                db.SaveChanges();
                _logger.LogInformation("Desired player {Number} added at {dateTime}", element.Playerid, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        public List<Desiredplayer> GetAll()
        {
            IQueryable<Desiredplayer> players = db.Desiredplayers;
            return players.Count() > 0 ? players.ToList() : null;
        }
        public void Update(Desiredplayer element)
        {
            try
            {
                db.Desiredplayers.Update(element);
                db.SaveChanges();
                _logger.LogInformation("Desired player {Number} updated at {dateTime}", element.Playerid, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        public void Delete(Desiredplayer element)
        {
            try
            {
                db.Desiredplayers.Remove(element);
                db.SaveChanges();
                _logger.LogInformation("Desired player {Number} deleted at {dateTime}", element.Playerid, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        public Desiredplayer GetPlayerByID(int id)
        {
            return db.Desiredplayers.Find(id);
        }

        public List<Desiredplayer> GetPlayersByManagement(Management element)
        {
            IQueryable<Desiredplayer> players = db.Desiredplayers.Where(needed =>
                needed.Managementid == element.Managementid
            );
            return players.Count() > 0 ? players.ToList() : null;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
