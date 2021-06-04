using System;
using System.Collections.Generic;
using System.IO;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace TechnologicalUI
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var _config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
            IUserInfoRepository rep = new UserInfoRepository(new transfersystemContext(Connection.GetConnection(0, _config)));
            Userinfo user = rep.FindUserByLogin("567");
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<Start>();
                    services.AddSingleton(provider => { return user; });
                    DiExtensions.AddRepositoryExtensions(services);
                    DiExtensions.AddControllerExtensions(services);
                    services.AddDbContext<transfersystemContext>(option => option.UseNpgsql(Connection.GetConnection(user.Permission, _config)));

                    var serilogLogger = new LoggerConfiguration()
                        .WriteTo.Console() // .File(_config["Logger"])
                        .CreateLogger();
                    services.AddLogging(x =>
                    {
                        x.AddSerilog(logger: serilogLogger, dispose: true);
                    });
                });

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var prog = services.GetRequiredService<Start>();
                    prog.Run();
                    Console.WriteLine("Successfully opened");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Occured");
                }
            }
        }
        
    }
    class Start
    {
        private UserController _user;
        private AnalyticController _analytic;
        private ManagerController _manager;
        private ModeratorController _moderator;
        public Start(UserController user, AnalyticController analytic, ManagerController manager, ModeratorController moderator)
        {
            _user = user;
            _analytic = analytic;
            _manager = manager;
            _moderator = moderator;
            Run();
        }
        public void Run()
        {
            int need = 0;
            while (need != 5)
            {
                PrintMenuMain();
                need = Convert.ToInt32(Console.ReadLine());
                switch (need)
                {
                    case 1:
                        CheckUser();
                        break;
                    case 2:
                        CheckAnalytic();
                        break;
                    case 3:
                        CheckManager();
                        break;
                    case 4:
                        CheckModerator();
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Неверный номер");
                        break;
                }
            }
        }
        void PrintMenuMain()
        {
            Console.WriteLine("1 - user\n2 - analytic\n3 - manager\n4 - moderator\n5 - Exit");
        }
        void CheckUser()
        {
            int need = 0;
            while (need != 6)
            {
                Console.WriteLine("1 - Просмотреть всех игроков\n2 - Получить игрока по ID\n3 - Просмотреть все" +
               " команды\n4 - Получить команду по ID\n5 - Получить статистику по ID\n6 - Exit");
                need = Convert.ToInt32(Console.ReadLine());
                switch (need)
                {
                    case 1:
                        GetAllPlayers();
                        break;
                    case 2:
                        GetPlayerByID();
                        break;
                    case 3:
                        GetAllTeams();
                        break;
                    case 4:
                        GetTeamByID();
                        break;
                    case 5:
                        GetStatByID();
                        break;
                    case 6:
                        break;
                    default:
                        Console.WriteLine("Неверный номер");
                        break;
                }
            }
        }
        void GetAllPlayers()
        {
            List<Player> players = _user.GetAllPlayers();
            if (players != null)
            {
                foreach (Player player in players)
                {

                    Console.WriteLine(Convert.ToString(player.Playerid) + " " +
                        Convert.ToString(player.Teamid) + " " +
                        Convert.ToString(player.Statistics) + " " +
                        Convert.ToString(player.Name) + " " +
                        Convert.ToString(player.Position) + " " +
                        Convert.ToString(player.Weight) + " " +
                        Convert.ToString(player.Height) + " " +
                        Convert.ToString(player.Number) + " " +
                        Convert.ToString(player.Age) + " " +
                        Convert.ToString(player.Country) + " " +
                        Convert.ToString(player.Cost));
                }
            }
            else
            {
                Console.WriteLine("Игроки не найдены");
            }
        }
        void GetPlayerByID()
        {
            Console.WriteLine("Введите ID:");
            int id = Convert.ToInt32(Console.ReadLine());
            Player player = _user.FindPlayerByID(id);
            if (player != null)
            {
                Console.WriteLine(Convert.ToString(player.Playerid) + " " +
                        Convert.ToString(player.Teamid) + " " +
                        Convert.ToString(player.Statistics) + " " +
                        Convert.ToString(player.Name) + " " +
                        Convert.ToString(player.Position) + " " +
                        Convert.ToString(player.Weight) + " " +
                        Convert.ToString(player.Height) + " " +
                        Convert.ToString(player.Number) + " " +
                        Convert.ToString(player.Age) + " " +
                        Convert.ToString(player.Country) + " " +
                        Convert.ToString(player.Cost));
            }
            else
            {
                Console.WriteLine("Игрок не найден");
            }
        }
        void GetAllTeams()
        {
            List<Team> teams = _user.GetAllTeams();
            if (teams != null)
            {
                foreach (Team team in teams)
                {
                    Console.WriteLine(Convert.ToString(team.Teamid) + " " +
                        Convert.ToString(team.Managementid) + " " +
                        Convert.ToString(team.Name) + " " +
                        Convert.ToString(team.Headcoach) + " " +
                        Convert.ToString(team.Country) + " " +
                        Convert.ToString(team.Stadium) + " " +
                        Convert.ToString(team.Balance));
                }
            }
            else
            {
                Console.WriteLine("Команды не найдены");
            }
        }
        void GetTeamByID()
        {
            Console.WriteLine("Введите ID:");
            int id = Convert.ToInt32(Console.ReadLine());
            Team team = _user.FindTeamByID(id);
            if (team != null)
            {
                Console.WriteLine(Convert.ToString(team.Teamid) + " " +
                        Convert.ToString(team.Managementid) + " " +
                        Convert.ToString(team.Name) + " " +
                        Convert.ToString(team.Headcoach) + " " +
                        Convert.ToString(team.Country) + " " +
                        Convert.ToString(team.Stadium) + " " +
                        Convert.ToString(team.Balance));
            }
            else
            {
                Console.WriteLine("Команда не найдена");
            }
        }
        void GetStatByID()
        {
            Console.WriteLine("Введите ID:");
            int id = Convert.ToInt32(Console.ReadLine());
            Statistic stat = _user.GetPlayerStatistic(id);
            if (stat != null)
            {
                Console.WriteLine(Convert.ToString(stat.Statisticsid) + " " +
                        Convert.ToString(stat.Numberofwashers) + " " +
                        Convert.ToString(stat.Averagegametime));
            }
            else
            {
                Console.WriteLine("Статистика не найдена");
            }
        }
        void CheckAnalytic()
        {
            int need = 0;
            while (need != 2)
            {
                Console.WriteLine("1 - Просмотреть желаемых игроков\n2 - Exit");
                need = Convert.ToInt32(Console.ReadLine());
                switch (need)
                {
                    case 1:
                        GetDesiredPlayers();
                        break;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("Неверный номер");
                        break;
                }
            }
        }
        void GetDesiredPlayers()
        {
            List<Desiredplayer> players = _analytic.GetAllDesiredPlayers();
            if (players != null)
            {
                foreach (Desiredplayer player in players)
                {
                    Console.WriteLine(Convert.ToString(player.Id) + " " +
                        Convert.ToString(player.Playerid) + " " +
                        Convert.ToString(player.Teamid));
                }
            }
            else
            {
                Console.WriteLine("Игроки не найдены");
            }
        }
        void CheckManager()
        {
            int need = 0;
            while (need != 3)
            {
                Console.WriteLine("1 - Получить входящие сделки\n2 - Получить исходящие сделки\n3 - Exit");
                need = Convert.ToInt32(Console.ReadLine());
                switch (need)
                {
                    case 1:
                        GetIncomingDeals();
                        break;
                    case 2:
                        GetOutcomingDeals();
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Неверный номер");
                        break;
                }
            }
        }
        void GetIncomingDeals()
        {
            List<Availabledeal> deals = _manager.GetIncomingDeals();
            if (deals != null)
            {
                foreach (Availabledeal deal in deals)
                {
                    Console.WriteLine(Convert.ToString(deal.Id) + " " +
                        Convert.ToString(deal.Playerid) + " " +
                        Convert.ToString(deal.Tomanagementid) + " " +
                        Convert.ToString(deal.Frommanagementid) + " " +
                        Convert.ToString(deal.Cost) + " " +
                        Convert.ToString(deal.Status));
                }
            }
            else
            {
                Console.WriteLine("Сделки не найдены");
            }
        }
        void GetOutcomingDeals()
        {
            List<Availabledeal> deals = _manager.GetOutgoaingDeals();
            if (deals != null)
            {
                foreach (Availabledeal deal in deals)
                {
                    Console.WriteLine(Convert.ToString(deal.Id) + " " +
                        Convert.ToString(deal.Playerid) + " " +
                        Convert.ToString(deal.Tomanagementid) + " " +
                        Convert.ToString(deal.Frommanagementid) + " " +
                        Convert.ToString(deal.Cost) + " " +
                        Convert.ToString(deal.Status));
                }
            }
            else
            {
                Console.WriteLine("Сделки не найдены");
            }
        }
        void CheckModerator()
        {
            int need = 0;
            while (need != 2)
            {
                Console.WriteLine("1 - Просмотреть все сделки\n2 - Exit");
                need = Convert.ToInt32(Console.ReadLine());
                switch (need)
                {
                    case 1:
                        GetAllDeals();
                        break;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("Неверный номер");
                        break;
                }
            }
        }
        void GetAllDeals()
        {
            List<Availabledeal> deals = _moderator.GetAllDeals();
            if (deals != null)
            {
                foreach (Availabledeal deal in deals)
                {
                    Console.WriteLine(Convert.ToString(deal.Id) + " " +
                        Convert.ToString(deal.Playerid) + " " +
                        Convert.ToString(deal.Tomanagementid) + " " +
                        Convert.ToString(deal.Frommanagementid) + " " +
                        Convert.ToString(deal.Cost) + " " +
                        Convert.ToString(deal.Status));
                }
            }
            else
            {
                Console.WriteLine("Сделки не найдены");
            }
        }
    }
    public static class DiExtensions
    {
        public static void AddRepositoryExtensions(IServiceCollection services)
        {
            services.AddSingleton<IPlayerRepository, PlayerRepository>();
            services.AddSingleton<ITeamRepository, TeamRepository>();
            services.AddSingleton<IUserInfoRepository, UserInfoRepository>();
            services.AddSingleton<IStatisticsRepository, StatisticsRepository>();
            services.AddSingleton<IAvailableDealsRepository, AvailableDealsRepository>();
            services.AddSingleton<IDesiredPlayersRepository, DesiredPlayersRepository>();
            services.AddSingleton<IManagementRepository, ManagementRepository>();
        }
        public static void AddControllerExtensions(IServiceCollection services)
        {
            services.AddScoped<AnalyticController>();
            services.AddScoped<ManagerController>();
            services.AddScoped<ModeratorController>();
            services.AddScoped<UserController>();
        }
    }
}

