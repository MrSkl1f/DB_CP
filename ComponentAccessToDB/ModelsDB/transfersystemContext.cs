using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class transfersystemContext : DbContext
    {
        private string ConnectionString { get; set; }
        public transfersystemContext(string conn)
        {
            ConnectionString = conn;
        }

        public transfersystemContext(DbContextOptions<transfersystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Availabledeal> Availabledeals { get; set; }
        public virtual DbSet<Desiredplayer> Desiredplayers { get; set; }
        public virtual DbSet<Management> Managements { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Statistic> Statistics { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Userinfo> Userinfos { get; set; }
        public IQueryable<PlayersTeamStat> getplayers() => FromExpression(() => getplayers());
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.HasDbFunction(() => getplayers());

            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Availabledeal>(entity =>
            {
                entity.ToTable("availabledeals");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Frommanagementid).HasColumnName("frommanagementid");

                entity.Property(e => e.Playerid).HasColumnName("playerid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Tomanagementid).HasColumnName("tomanagementid");

                entity.HasOne(d => d.Frommanagement)
                    .WithMany(p => p.AvailabledealFrommanagements)
                    .HasForeignKey(d => d.Frommanagementid)
                    .HasConstraintName("availabledeals_frommanagementid_fkey");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Availabledeals)
                    .HasForeignKey(d => d.Playerid)
                    .HasConstraintName("availabledeals_playerid_fkey");

                entity.HasOne(d => d.Tomanagement)
                    .WithMany(p => p.AvailabledealTomanagements)
                    .HasForeignKey(d => d.Tomanagementid)
                    .HasConstraintName("availabledeals_tomanagementid_fkey");
            });

            modelBuilder.Entity<Desiredplayer>(entity =>
            {
                entity.ToTable("desiredplayers");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Managementid).HasColumnName("managementid");

                entity.Property(e => e.Playerid).HasColumnName("playerid");

                entity.Property(e => e.Teamid).HasColumnName("teamid");

                entity.HasOne(d => d.Management)
                    .WithMany(p => p.Desiredplayers)
                    .HasForeignKey(d => d.Managementid)
                    .HasConstraintName("desiredplayers_managementid_fkey");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Desiredplayers)
                    .HasForeignKey(d => d.Playerid)
                    .HasConstraintName("desiredplayers_playerid_fkey");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Desiredplayers)
                    .HasForeignKey(d => d.Teamid)
                    .HasConstraintName("desiredplayers_teamid_fkey");
            });

            modelBuilder.Entity<Management>(entity =>
            {
                entity.ToTable("management");

                entity.Property(e => e.Managementid)
                    .ValueGeneratedNever()
                    .HasColumnName("managementid");

                entity.Property(e => e.Analysistid).HasColumnName("analysistid");

                entity.Property(e => e.Managerid).HasColumnName("managerid");

                entity.HasOne(d => d.Analysist)
                    .WithMany(p => p.ManagementAnalysists)
                    .HasForeignKey(d => d.Analysistid)
                    .HasConstraintName("management_analysistid_fkey");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.ManagementManagers)
                    .HasForeignKey(d => d.Managerid)
                    .HasConstraintName("management_managerid_fkey");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("player");

                entity.Property(e => e.Playerid)
                    .ValueGeneratedNever()
                    .HasColumnName("playerid");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("position");

                entity.Property(e => e.Statistics).HasColumnName("statistics");

                entity.Property(e => e.Teamid).HasColumnName("teamid");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.StatisticsNavigation)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.Statistics)
                    .HasConstraintName("player_statistics_fkey");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.Teamid)
                    .HasConstraintName("player_teamid_fkey");
            });

            modelBuilder.Entity<Statistic>(entity =>
            {
                entity.HasKey(e => e.Statisticsid)
                    .HasName("statistics_pkey");

                entity.ToTable("statistics");

                entity.Property(e => e.Statisticsid)
                    .ValueGeneratedNever()
                    .HasColumnName("statisticsid");

                entity.Property(e => e.Averagegametime).HasColumnName("averagegametime");

                entity.Property(e => e.Numberofwashers).HasColumnName("numberofwashers");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("team");

                entity.Property(e => e.Teamid)
                    .ValueGeneratedNever()
                    .HasColumnName("teamid");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("country");

                entity.Property(e => e.Headcoach)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("headcoach");

                entity.Property(e => e.Managementid).HasColumnName("managementid");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.Stadium)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("stadium");

                entity.HasOne(d => d.Management)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.Managementid)
                    .HasConstraintName("team_managementid_fkey");
            });

            modelBuilder.Entity<Userinfo>(entity =>
            {
                entity.ToTable("userinfo");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("hash");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("login");

                entity.Property(e => e.Permission).HasColumnName("permission");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
