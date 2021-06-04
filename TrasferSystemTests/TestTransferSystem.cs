using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComponentAccessToDB;
using System.Collections.Generic;

namespace TrasferSystemTests
{
    [TestClass]
    public class TestTransferSystem
    {
        [TestMethod]
        public void TestUserInfoRepository()
        {
            IUserInfoRepository rep = new UserInfoRepository(new transfersystemContext(Connection.GetConnection((int)Permissions.Moder)));
            Userinfo user = new Userinfo { Hash = "3456", Login = "3456" };
            rep.Add(user);
            Userinfo checkUser1 = rep.FindUserByLogin("3456");
            Assert.IsNotNull(checkUser1, "user1 was not added");
            Assert.AreEqual("3456", checkUser1.Hash, "Not equal Added user");
            
            int userId = checkUser1.Id;
            user.Id = userId;
            checkUser1.Hash = "7890";
            rep.Update(checkUser1);
            Userinfo checkUser2 = rep.FindUserByID(userId);
            Assert.IsNotNull(checkUser2, "cannot find user2 by id");
            Assert.AreEqual("7890", checkUser2.Hash, "Not Equal Updated user");

            rep.Delete(checkUser2);
            Assert.IsNull(rep.FindUserByID(checkUser2.Id), "user2 was not deleted");    

            List<Userinfo> users = rep.GetAll();
            Assert.IsNotNull(users, "Can't find userinfos");
        }

        [TestMethod]
        public void TestTeamRepository()
        {
            ITeamRepository rep = new TeamRepository(new transfersystemContext(Connection.GetConnection((int)Permissions.Moder)));
            Team team = new Team { Managementid = 1, Name = "Dynamo", Headcoach = "Sklif", Country = "Russia", Stadium = "VTB", Balance = 100000 };
            rep.Add(team);
            Team checkTeam1 = rep.FindTeamByName("Dynamo");
            Assert.IsNotNull(checkTeam1, "team1 was not added");
            Assert.AreEqual("Dynamo", checkTeam1.Name, "Not equal Added team");
            
            int teamID = team.Teamid;
            checkTeam1.Headcoach = "Denis Sklif";
            rep.Update(checkTeam1);
            Team checkTeam2 = rep.FindTeamByID(teamID);
            Assert.IsNotNull(checkTeam2, "cannot find team2 by id");
            Assert.AreEqual("Denis Sklif", checkTeam2.Headcoach, "Not equal updated team");
            
            rep.Delete(checkTeam2);
            Assert.IsNull(rep.FindTeamByID(checkTeam2.Teamid), "team2 was not deleted");
            
            List<Team> teams = rep.GetAll();
            Assert.IsNotNull(teams, "Can't find teams");

            Player player = new Player { Playerid = 1, Teamid = 1, Statistics = 1, Name = "Denis", Position = "ca", Weight = 92, Height = 192, Number = 21, Age = 20, Country = "Russia", Cost = 2000 };
            Team checkTeam3 = rep.FindTeamByPlayer(player);
            Assert.IsNotNull(checkTeam3, "Can't find team by player");
        }

        [TestMethod]
        public void TestPlayerRepository()
        {
            IPlayerRepository rep = new PlayerRepository(new transfersystemContext(Connection.GetConnection((int)Permissions.Moder)));
            Player player = new Player { Teamid = 1, Statistics = 1, Name = "Vlad", Position = "ca", Weight = 76, Height = 172, Number = 7, Age = 27, Country = "Russia", Cost = 4000 };
            rep.Add(player);
            Player checkPlayer1 = rep.FindPlayerByName("Vlad");
            Assert.IsNotNull(checkPlayer1, "player1 was no added");
            Assert.AreEqual("Vlad", checkPlayer1.Name, "Not equal added player");
            
            int playerID = checkPlayer1.Playerid;
            checkPlayer1.Height = 176;
            rep.Update(checkPlayer1);
            Player checkPlayer2 = rep.FindPlayerByID(playerID);
            Assert.IsNotNull(checkPlayer2, "player2 was not found by id");
            Assert.AreEqual(176, checkPlayer2.Height, "player2 was not updated");

            rep.Delete(checkPlayer2);
            Assert.IsNull(rep.FindPlayerByID(playerID), "player2 was not deleted");
            
            List<Player> players = rep.GetAll();
            Assert.IsNotNull(players, "Can't find teams");

            Team team = new Team { Teamid = 1 };
            players = rep.GetPlayersByTeam(team);
            Assert.IsNotNull(players, "can't find players by team");
        }
        
        [TestMethod]
        public void TestStatisticsRepository()
        {
            IStatisticsRepository rep = new StatisticsRepository(new transfersystemContext(Connection.GetConnection((int)Permissions.Moder)));
            Statistic stat = new Statistic { Averagegametime = 20, Numberofwashers = 5 };
            rep.Add(stat);
            
            Statistic checkStat1 = rep.GetStatisticByID(2);
            Assert.IsNotNull(checkStat1, "stat1 was not added");
            Assert.AreEqual(5, checkStat1.Numberofwashers, "not equal added stat1");

            checkStat1.Numberofwashers = 25;
            rep.Update(checkStat1);
            Statistic checkStat2 = rep.GetStatisticByID(2);
            Assert.AreEqual(25, checkStat2.Numberofwashers, "stat2 was not updated");

            rep.Delete(checkStat2);
            Assert.IsNull(rep.GetStatisticByID(2), "stat2 was not deleted");

            List<Statistic> stats = rep.GetAll();
            Assert.IsNotNull(stats, "Can't find stats");

            Player player = new Player { Playerid = 1, Statistics = 1 };
            Assert.IsNotNull(rep.GetStatisticsByPlayer(player), "can't find stat by player");
        }
        [TestMethod]
        public void TestAvailableDealsRepository()
        {
            IAvailableDealsRepository rep = new AvailableDealsRepository(new transfersystemContext(Connection.GetConnection((int)Permissions.Moder)));
            Availabledeal deal = new Availabledeal { Playerid = 1, Frommanagementid = 2, Tomanagementid = 3, Cost = 2000, Status = 2 };
            rep.Add(deal);
            
            deal.Cost = 2500;
            rep.Update(deal);
            
            List<Availabledeal> deals = rep.GetAll();
            Assert.IsNotNull(deals, "can't find deals");

            deals = rep.GetIncomingDeals(new Management { Managementid = 4 });
            Assert.IsNotNull(deals, "can't find incoming deals");

            deals = rep.GetOutgoaingDeals(new Management { Managementid = 3 });
            Assert.IsNotNull(deals, "can't find incoming deals");

            deal.Id = 1;
            rep.ConfirmDeal(deal);
            Availabledeal resDeal = rep.GetDealByID(1);
            Assert.IsNotNull(resDeal, "can't find deal");
            Assert.AreEqual(resDeal.Status, 0, "status is different");
            
            rep.RejectDeal(resDeal);
            resDeal = rep.GetDealByID(1);
            Assert.AreEqual(resDeal.Status, 1, "status is different");

            rep.Delete(deal);
        }
        [TestMethod]
        public void TestDesiredPlayersRepository()
        {
            IDesiredPlayersRepository rep = new DesiredPlayersRepository(new transfersystemContext(Connection.GetConnection((int)Permissions.Moder)));
            Desiredplayer player = new Desiredplayer { Playerid = 1, Teamid = 1, Managementid = 3 };
            rep.Add(player);
            
            player.Managementid = 2;
            rep.Update(player);

            List<Desiredplayer> players = rep.GetAll();
            Assert.IsNotNull(player, "Can't find players");

            player = rep.GetPlayerByID(1);
            Assert.IsNotNull(player, "can't find player");

            players = rep.GetPlayersByManagement(new Management { Managementid = 3 });
            Assert.IsNotNull(player, "Can't find players");

            rep.Delete(player);
        }

        [TestMethod]
        public void Test()
        {
            IUserInfoRepository rep = new UserInfoRepository(new transfersystemContext((Connection.GetConnection(0))));
            //Userinfo user = new Userinfo { Hash = "567", Login = "567", Permission = 3 };
            //rep.Add(user);
        }
    }
}
