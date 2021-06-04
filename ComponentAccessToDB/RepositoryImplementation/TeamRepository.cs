using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentAccessToDB
{
    public class TeamRepository : ITeamRepository, IDisposable
    {
        private readonly transfersystemContext db;
        private readonly ILogger<TeamRepository> _logger;
        public TeamRepository(transfersystemContext curDb, ILogger<TeamRepository> logger)
        {
            db = curDb;
            _logger = logger;
        }
        public void Add(Team element)
        {
            try
            {
                element.Teamid = db.Teams.Count() + 1;
                db.Teams.Add(element);
                db.SaveChanges();
                _logger.LogInformation("Team {Name} added at {dateTime}", element.Name, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        public List<Team> GetAll()
        {
            IQueryable<Team> teams = db.Teams;
            return teams.Count() > 0 ? teams.ToList() : null;
        }
        public void Update(Team element)
        {
            try
            {
                db.Teams.Update(element);
                db.SaveChanges();
                _logger.LogInformation("Team {Name} updated at {dateTime}", element.Name, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        public void Delete(Team element)
        {
            try
            {
                db.Teams.Remove(element);
                db.SaveChanges();
                _logger.LogInformation("Team {Name} removed at {dateTime}", element.Name, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        public Team FindTeamByID(int id)
        {
            return db.Teams.Find(id);
        }
        public Team FindTeamByName(string name)
        {
            IQueryable<Team> teams = db.Teams.Where(needed => needed.Name == name);
            return teams.Count() > 0 ? teams.First() : null; 
        }
        public Team FindTeamByPlayer(Player player)
        {
            IQueryable<Team> teams = db.Teams.Where(needed => needed.Players.Contains(player));
            return teams.Count() > 0 ? teams.First() : null;
        }
        public Team FindTeamByManagement(Management management)
        {
            IQueryable<Team> teams = db.Teams.Where(needed => needed.Managementid == management.Managementid);
            return teams.Count() > 0 ? teams.First() : null;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
