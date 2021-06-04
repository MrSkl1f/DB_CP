using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentAccessToDB;
using Microsoft.Extensions.Logging;

namespace ComponentBuisinessLogic
{
    public class ModeratorController : UserController
    {
        IAvailableDealsRepository dealsRepository;
        public ModeratorController(Userinfo user, ILogger<UserController> logger, IAvailableDealsRepository dealsRep, IPlayerRepository playerRep, ITeamRepository teamRep, IManagementRepository managementRep, IDesiredPlayersRepository desiredPlayerRep, IStatisticsRepository statRep) :
            base(user, logger, playerRep, teamRep, managementRep, desiredPlayerRep, statRep)
        {
            dealsRepository = dealsRep;
        }
        public bool MakeDeal(int dealID)
        {
            Availabledeal deal = dealsRepository.GetDealByID(dealID);
            if (deal == null)
            {
                return false;
            }
            Team newTeam = teamRepository.FindTeamByManagement(deal.Tomanagement);
            if (newTeam == null)
            {
                return false;
            }
            Team lastTeam = teamRepository.FindTeamByManagement(deal.Frommanagement);
            if (lastTeam == null)
            {
                return false;
            }
            if (! CheckOportunityToBuy(deal.Cost, newTeam))
            {
                return false;
            }
            UpdateTeamBalance(lastTeam, newTeam, deal.Cost);
            UpdatePlayerTeam(deal.Player, newTeam.Teamid);
            dealsRepository.Delete(deal);
            return true;
        }
        public bool DeleteDeal(int dealID)
        {
            Availabledeal deal = dealsRepository.GetDealByID(dealID);
            if (deal == null)
            {
                return false;
            }
            dealsRepository.Delete(deal);
            return true;
        }
        public void UpdatePlayerTeam(Player player, int team)
        {
            player.Teamid = team;
            playerRepository.Update(player);
        }
        private void UpdateTeamBalance(Team lastTeam, Team newTeam, int cost)
        {
            lastTeam.Balance -= cost;
            newTeam.Balance += cost;
            teamRepository.Update(lastTeam);
            teamRepository.Update(newTeam);
        }
        private void DeleteDeal(Availabledeal deal)
        {
            dealsRepository.Delete(deal);
        }
        private bool CheckOportunityToBuy(int cost, Team team)
        {
            return cost < team.Balance;
        }
        public List<Availabledeal> GetAllDeals()
        {
            return dealsRepository.GetAll();
        }
    }
}
