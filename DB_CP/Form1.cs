using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using Serilog.Core;
using Microsoft.Extensions.Logging;

namespace DB_CP
{
    public partial class Form1 : Form
    {
        private readonly ILogger<Form1> _logger;
        private readonly AnalyticController _analytic;
        private readonly ManagerController _manager;
        private readonly ModeratorController _moderator;
        private readonly UserController _userController;
        private readonly Userinfo _user;
        public Form1(ILogger<Form1> logger, Userinfo currentUser, AnalyticController analytic, ManagerController manager, ModeratorController moderator, UserController userController)
        {
            _analytic = analytic;
            _manager = manager;
            _moderator = moderator;
            _userController = userController;
            _logger = logger;
            _user = currentUser;
            InitializeComponent();
            CheckPerms();
        }
        private void CheckPerms()
        {
            if (_user.Permission == (int)Permissions.Analytic)
            {
                ManagerGroup.Enabled = false;
                ModeratorGroup.Enabled = false;
            }
            else if (_user.Permission == (int)Permissions.Manager)
            {
                AnalyticGroup.Enabled = false;
                ModeratorGroup.Enabled = false;
            }
            else
            {
                AnalyticGroup.Enabled = false;
                ManagerGroup.Enabled = false;
            }
        }
        private void AddColumnsTeam()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("TeamID", "Team ID");
            dataGridView1.Columns.Add("ManagamanetID", "Managament ID");
            dataGridView1.Columns.Add("Name", "Team Name");
            dataGridView1.Columns.Add("Headcoach", "HeadCoach");
            dataGridView1.Columns.Add("Country", "Country");
            dataGridView1.Columns.Add("Stadium", "Stadium");
            dataGridView1.Columns.Add("Balance", "Balance");
        }
        private void AddColumnsStatistic()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("StatisticsId", "Statistics Id");
            dataGridView1.Columns.Add("Numberofwashers", "Number of washers");
            dataGridView1.Columns.Add("Averagegametime", "Average game time");
        }
        private void AddColumnsPlayer()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("PlayerId", "Player ID");
            dataGridView1.Columns.Add("TeamId", "Team ID");
            dataGridView1.Columns.Add("Statistics", "Statistics");
            dataGridView1.Columns.Add("Name", "Player Name");
            dataGridView1.Columns.Add("Position", "Position");
            dataGridView1.Columns.Add("Weight", "Weight");
            dataGridView1.Columns.Add("Height", "Height");
            dataGridView1.Columns.Add("Number", "Number");
            dataGridView1.Columns.Add("Age", "Age");
            dataGridView1.Columns.Add("Country", "Country");
            dataGridView1.Columns.Add("Cost", "Cost");
        }
        private void AddColumnsDesiredPlayer()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Id", "ID");
            dataGridView1.Columns.Add("PlayerId", "Player ID");
            dataGridView1.Columns.Add("TeamId", "Team ID");
            dataGridView1.Columns.Add("ManagementID", "Management ID");
        }
        private void AddColumnsAvailableDeal()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Id", "ID");
            dataGridView1.Columns.Add("PlayerID", "Player ID");
            dataGridView1.Columns.Add("ToManagementId", "To Management ID");
            dataGridView1.Columns.Add("FromManagementId", "From Management ID");
            dataGridView1.Columns.Add("Cost", "Cost");
            dataGridView1.Columns.Add("Status", "Status");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Application.Exit();
        }
        private void GetAllPlayers_Click(object sender, EventArgs e)
        {
            AddColumnsPlayer();
            List<Player> players = _userController.GetAllPlayers();
            if (players != null)
            {
                foreach (Player player in players)
                {
                    dataGridView1.Rows.Add(player.Playerid, player.Teamid, player.Statistics, player.Name, player.Position,
                        player.Weight, player.Height, player.Number, player.Age, player.Country, player.Cost);
                }
            }
            else
            {
                MessageBox.Show("Игроки не найдены");
            }
        }
        private void GetPlayerByID_Click(object sender, EventArgs e)
        {
            if (PlayerIDBox.Text != "")
            {
                AddColumnsPlayer();
                Player player = _userController.FindPlayerByID(Convert.ToInt32(PlayerIDBox.Text));
                if (player != null)
                {
                    dataGridView1.Rows.Add(player.Playerid, player.Teamid, player.Statistics, player.Name, player.Position,
                        player.Weight, player.Height, player.Number, player.Age, player.Country, player.Cost);
                }
                else
                {
                    MessageBox.Show("Игрок не найден");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели Id игрока");
            }
        }
        private void GetTeamByIDForPlayers_Click(object sender, EventArgs e)
        {
            if (TeamIDBoxForPlayer.Text != "")
            {
                AddColumnsPlayer();
                List<Player> players = _userController.GetPlayersByTeam(Convert.ToInt32(TeamIDBoxForPlayer.Text));
                if (players != null)
                {
                    foreach (Player player in players)
                    {
                        dataGridView1.Rows.Add(player.Playerid, player.Teamid, player.Statistics, player.Name, player.Position,
                            player.Weight, player.Height, player.Number, player.Age, player.Country, player.Cost);
                    }
                }
                else
                {
                    MessageBox.Show("Игроки не найдены");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели Id команды");
            }
        }
        private void GetPlayersByName_Click(object sender, EventArgs e)
        {
            if (PlayerName.Text != "")
            {
                AddColumnsPlayer();
                Player player = _userController.FindPlayerByName(PlayerName.Text);
                if (player != null)
                {
                    dataGridView1.Rows.Add(player.Playerid, player.Teamid, player.Statistics, player.Name, player.Position,
                        player.Weight, player.Height, player.Number, player.Age, player.Country, player.Cost);
                }
                else
                {
                    MessageBox.Show("Игроки не найден");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели Name игрока");
            }
        }
        private void GetAllTeams_Click(object sender, EventArgs e)
        {
            AddColumnsTeam();
            List<Team> teams = _userController.GetAllTeams();
            if (teams != null)
            {
                foreach (Team team in teams)
                {
                    dataGridView1.Rows.Add(team.Teamid, team.Managementid, team.Name, team.Headcoach, team.Country, team.Stadium, team.Balance);
                }
            }
            else
            {
                MessageBox.Show("Команды не найдены");
            }
        }
        private void GetTeamByID_Click(object sender, EventArgs e)
        {
            if (TeamID.Text != "")
            {
                AddColumnsTeam();
                Team team = _userController.FindTeamByID(Convert.ToInt32(TeamID.Text));
                if (team != null)
                {
                    dataGridView1.Rows.Add(team.Teamid, team.Managementid, team.Name, team.Headcoach, team.Country, team.Stadium, team.Balance);
                }
                else
                {
                    MessageBox.Show("Команда не найдена");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели Id команды");
            }
        }
        private void GetTeamByName_Click(object sender, EventArgs e)
        {
            if (TeamName.Text != "")
            {
                AddColumnsTeam();
                Team team = _userController.FindTeamByName(TeamName.Text);
                if (team != null)
                {
                    dataGridView1.Rows.Add(team.Teamid, team.Managementid, team.Name, team.Headcoach, team.Country, team.Stadium, team.Balance);
                }
                else
                {
                    MessageBox.Show("Команда не найдена");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели Name команды");
            }
        }
        private void GetStatisticsByID_Click(object sender, EventArgs e)
        {
            if (StatisticsID.Text != "")
            {
                AddColumnsStatistic();
                Statistic stat = _userController.GetPlayerStatistic(Convert.ToInt32(StatisticsID.Text));
                if (stat != null)
                {
                    dataGridView1.Rows.Add(stat.Statisticsid, stat.Numberofwashers, stat.Averagegametime);
                }
                else
                {
                    MessageBox.Show("Статистика не найдена");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели Id статистики");
            }
        }
        private void GetAllDesiredPlayers_Click(object sender, EventArgs e)
        {
            AddColumnsDesiredPlayer();
            List<Desiredplayer> players = _analytic.GetAllDesiredPlayers();
            if (players != null)
            {
                foreach (Desiredplayer player in players)
                {
                    dataGridView1.Rows.Add(player.Id, player.Playerid, player.Teamid, player.Managementid);
                }
            }
            else
            {
                MessageBox.Show("Игроки не найдены");
            }
        }
        private void AddDesiredPlayer_Click(object sender, EventArgs e)
        {
            if (PlayerIDForDesired.Text == "")
            {
                MessageBox.Show("Не указан ID");
                return;
            }
            if (_analytic.AddDesiredPlayer(Convert.ToInt32(PlayerIDForDesired.Text)) == false)
            {
                MessageBox.Show("Не удалось добавить игрока");
                return;
            }
            GetAllDesiredPlayers_Click(sender, e);
        }
        private void DeleteDesiredPlayer_Click(object sender, EventArgs e)
        {
            if (DesiredPlayerID.Text == "")
            {
                MessageBox.Show("Не указан ID");
                return;
            }
            if (_analytic.DeleteDesiredPlayer(Convert.ToInt32(DesiredPlayerID.Text)) == false)
            {
                MessageBox.Show("Не удалось удалить игрока");
                return;
            }
            GetAllDesiredPlayers_Click(sender, e);
        }
        private void RequestPlayer_Click(object sender, EventArgs e)
        {
            if (PlayerIDForManager.Text == "")
            {
                MessageBox.Show("Не указан ID");
                return;
            }
            if (CostForManager.Text == "")
            {
                MessageBox.Show("Не указана цена");
                return;
            }
            if (_manager.RequestPlayer(Convert.ToInt32(PlayerIDForManager.Text), Convert.ToInt32(CostForManager.Text)) == false) 
            {
                MessageBox.Show("Не удалось предложить игрока");
                return;
            }
        }
        private void ConfirmDeal_Click(object sender, EventArgs e)
        {
            if (DealID.Text == "")
            {
                MessageBox.Show("Не указан ID сделки");
                return;
            }
            if (_manager.ConfirmDeal(Convert.ToInt32(DealID.Text)) == false)
            {
                MessageBox.Show("Не удалось подтвердить сделку");
                return;
            }
        }
        private void RejectDeal_Click(object sender, EventArgs e)
        {
            if (DealID.Text == "")
            {
                MessageBox.Show("Не указан ID сделки");
                return;
            }
            if (_manager.RejectDeal(Convert.ToInt32(DealID.Text)) == false)
            {
                MessageBox.Show("Не удалось отклонить сделку");
                return;
            }
        }
        private void GetIncoming_Click(object sender, EventArgs e)
        {
            AddColumnsAvailableDeal();
            List<Availabledeal> deals = _manager.GetIncomingDeals();
            if (deals != null)
            {
                foreach (Availabledeal deal in deals)
                {
                    dataGridView1.Rows.Add(deal.Id, deal.Playerid, deal.Tomanagementid, deal.Frommanagementid, deal.Cost, deal.Status);
                }
            }   
            else
            {
                MessageBox.Show("Сделки не найдены");
            }
        }
        private void GetOutcoming_Click(object sender, EventArgs e)
        {
            AddColumnsAvailableDeal();
            List<Availabledeal> deals = _manager.GetOutgoaingDeals();
            if (deals != null)
            {
                foreach (Availabledeal deal in deals)
                {
                    dataGridView1.Rows.Add(deal.Id, deal.Playerid, deal.Tomanagementid, deal.Frommanagementid, deal.Cost, deal.Status);
                }
            }
            else
            {
                MessageBox.Show("Сделки не найдены");
            }
        }
        private void MakeDeal_Click(object sender, EventArgs e)
        {
            if (DealIDForModer.Text == "")
            {
                MessageBox.Show("Не указан ID сделки");
            }
            else
            {
                if (!_moderator.MakeDeal(Convert.ToInt32(DealIDForModer.Text)))
                {
                    MessageBox.Show("Не удалось провести сделку");
                }
            }
        }
        private void DeleteDeal_Click(object sender, EventArgs e)
        {
            if (DealIDForModer.Text == "")
            {
                MessageBox.Show("Не указан ID сделки");
            }
            else
            {
                if (!_moderator.DeleteDeal(Convert.ToInt32(DealIDForModer.Text)))
                {
                    MessageBox.Show("Не удалось удалить сделку");
                }
            }
        }
        private void GetAllDeals_Click(object sender, EventArgs e)
        {
            AddColumnsAvailableDeal();
            List<Availabledeal> deals = _moderator.GetAllDeals();
            if (deals != null)
            {
                foreach (Availabledeal deal in deals)
                {
                    dataGridView1.Rows.Add(deal.Id, deal.Playerid, deal.Tomanagementid, deal.Frommanagementid, deal.Cost, deal.Status);
                }
            }
            else
            {
                MessageBox.Show("Сделки не найдены");
            }
        }
    }
}
