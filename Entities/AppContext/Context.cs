using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.AppContext
{
    public class Context: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Edition> Editions{ get; set; }
        public DbSet<Category> Categories{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                var server = "localhost";
                var user = "sa";
                var catalog = "kingofthetowerDB";
                var password = "@.J0s3RmP4ss";

                var stringConnection = $"Server=tcp:{server},1433;Initial Catalog={catalog};" +
                        $" PersistSecurityInfo = False;User ID={user}; Password={password}; " +
                        " MultipleActiveResultSets = False; Encrypt=True; " +
                        " TrustServerCertificate=True; Connection Timeout=30;";

                optionsBuilder.UseSqlServer(stringConnection);

            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Constraints
            // modelBuilder.Entity<Player>().HasIndex(p => p.NIF).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Mail).IsUnique();

            modelBuilder.Entity<Game>(entry => { entry.ToTable("Games", tb => tb.HasTrigger("T_update_game")); });
            #endregion

            #region Categories default values
            
            var catMasc = new Category() { Id = 1, Name = "Male" };
            var catFem = new Category() { Id = 2, Name = "Female" };
            var catMin = new Category() { Id = 3, Name = "Mini" };
            
            #endregion

            #region Editions default values
            
            var edit1 = new Edition() { Id = 1, Name = "27082022", IsActive = false };
            var edit2 = new Edition() { Id = 2, Name = "26022023", IsActive = false };
            var edit3 = new Edition() { Id = 3, Name = "05082023", IsActive = false};
            var edit4 = new Edition() { Id = 4, Name = "27072024", IsActive = true};

            #endregion

            #region Group default values

            var groupMascA = new Group() { Id = 1, Name = "MasculinoA" };
            var groupMascB = new Group() { Id = 2, Name = "MasculinoB" };
            var groupMascC = new Group() { Id = 3, Name = "MasculinoC" };
            var groupFemA = new Group() { Id = 4, Name = "FemeninoA" };
            var groupFemB = new Group() { Id = 5, Name = "FemeninoB" };

            #endregion

            #region User default values

            var admin = new User() { Id = 1, Name = "José Ramón", Mail = "kingofthetower3x3@gmail.com", Role = "admin" };
            var staff = new User() { Id = 2, Name = "José Ramón", Mail = "jorapijo42@gmail.com", Role = "staff" };
            var user = new User() { Id = 3, Name = "José Ramón", Mail = "jorapijo@gmail.com", Role = "base_user" };

            #endregion

            #region Teams default values

            var teamMasc1A = new Team() { Id = 1, Name = "teamMasc1A", CategoryId = catMasc.Id, Pay = false, Wins = 0, Defeats = 2, Points_diff = -33,  Classification_points = 0, GroupId = groupMascA.Id, EditionId = edit1.Id };
            var teamMasc2A = new Team() { Id = 2, Name = "teamMasc2A", CategoryId = catMasc.Id, Pay = false, Wins = 1, Defeats = 0, Points_diff = 13,   Classification_points = 3, GroupId = groupMascA.Id, EditionId = edit1.Id };
            var teamMasc3A = new Team() { Id = 3, Name = "teamMasc3A", CategoryId = catMasc.Id, Pay = false, Wins = 1, Defeats = 0, Points_diff = 20,   Classification_points = 3, GroupId = groupMascA.Id, EditionId = edit1.Id };

            var teamMasc1B = new Team() { Id = 4, Name = "teamMasc1B", CategoryId = catMasc.Id, Pay = false, Wins = 0, Defeats = 0, Points_diff = 0, Classification_points = 0, GroupId = groupMascB.Id, EditionId = edit1.Id };
            var teamMasc2B = new Team() { Id = 5, Name = "teamMasc2B", CategoryId = catMasc.Id, Pay = false, Wins = 0, Defeats = 0, Points_diff = 0, Classification_points = 0, GroupId = groupMascB.Id, EditionId = edit1.Id };
            var teamMasc3B = new Team() { Id = 6, Name = "teamMasc3B", CategoryId = catMasc.Id, Pay = false, Wins = 0, Defeats = 0, Points_diff = 0, Classification_points = 0, GroupId = groupMascB.Id, EditionId = edit1.Id  };
            
            var teamMasc1C = new Team() { Id = 7, Name = "teamMasc1C", CategoryId = catMasc.Id, Pay = false, Wins = 0, Defeats = 0, Points_diff = 0, Classification_points = 0, GroupId = groupMascC.Id, EditionId = edit1.Id };
            var teamMasc2C = new Team() { Id = 8, Name = "teamMasc2C", CategoryId = catMasc.Id, Pay = false, Wins = 0, Defeats = 0, Points_diff = 0, Classification_points = 0, GroupId = groupMascC.Id, EditionId = edit1.Id };
            var teamMasc3C = new Team() { Id = 9, Name = "teamMasc3C", CategoryId = catMasc.Id, Pay = false, Wins = 0, Defeats = 0, Points_diff = 0, Classification_points = 0, GroupId = groupMascC.Id, EditionId = edit1.Id };

            var teamFem1A = new Team() { Id = 10, Name = "teamFem1A", CategoryId = catFem.Id, Pay = false, Wins = 0, Defeats = 0, Points_diff = 0, Classification_points = 0, GroupId = groupFemA.Id, EditionId = edit1.Id  };
            var teamFem2A = new Team() { Id = 11, Name = "teamFem2A", CategoryId = catFem.Id, Pay = false, Wins = 0, Defeats = 0, Points_diff = 0, Classification_points = 0, GroupId = groupFemA.Id, EditionId = edit1.Id  };
            var teamFem1B = new Team() { Id = 12, Name = "teamFem1B", CategoryId = catFem.Id, Pay = false, Wins = 0, Defeats = 0, Points_diff = 0, Classification_points = 0, GroupId = groupFemB.Id, EditionId = edit1.Id  };
            var teamFem2B = new Team() { Id = 13, Name = "teamFem2B", CategoryId = catFem.Id, Pay = false, Wins = 0, Defeats = 0, Points_diff = 0, Classification_points = 0, GroupId = groupFemB.Id, EditionId = edit1.Id  };

            #endregion

            #region Players default values

            var playerM1A2 = new Player() { Id = 3, NIF = "12354678A", Name = "playerM1A2", Phone = "111222333", Instagram = "playerIg", WantPics = true, TeamId = teamMasc1A.Id };
            var playerM1A3 = new Player() { Id = 4, NIF = "12354678B", Name = "playerM1A3", Phone = "111222333", Instagram = "playerIg", WantPics = true, TeamId = teamMasc1A.Id };
            var playerM2A1 = new Player() { Id = 5, NIF = "12354678C", Name = "playerM2A1", Phone = "111222333", Instagram = "playerIg", WantPics = true, TeamId = teamMasc2A.Id };
            var playerM1A1 = new Player() { Id = 1, NIF = "12354678Z", Name = "playerM1A1", Phone = "111222333", Instagram = "playerIg", WantPics = true, TeamId = teamMasc1A.Id };
            var playerM2A2 = new Player() { Id = 6, NIF = "12354678D", Name = "playerM2A2", Phone = "111222333", Instagram = "playerIg", WantPics = true, TeamId = teamMasc2A.Id };
            var playerM2A3 = new Player() { Id = 7, NIF = "12354678E", Name = "playerM2A3", Phone = "111222333", Instagram = "playerIg", WantPics = true, TeamId = teamMasc2A.Id };
            var playerM3A1 = new Player() { Id = 8, NIF = "12354678F", Name = "playerM3A1", Phone = "111222333", Instagram = "playerIg", WantPics = true, TeamId = teamMasc3A.Id };
            var playerM3A2 = new Player() { Id = 9, NIF = "12354678G", Name = "playerM3A2", Phone = "111222333", Instagram = "playerIg", WantPics = true, TeamId = teamMasc3A.Id };
            var playerM3A3 = new Player() { Id = 10, NIF = "12354678H", Name = "playerM3A3", Phone = "111222333", Instagram = "playerIg", WantPics = true, TeamId = teamMasc3A.Id };
            var playerF1A1 = new Player() { Id = 11, NIF = "12354678I", Name = "playerF1A1", Phone = "111222333", Instagram = "playerIg", WantPics = true, TeamId = teamFem1A.Id };
            var playerF1A2 = new Player() { Id = 12, NIF = "12354678J", Name = "playerF1A2", Phone = "111222333", Instagram = "playerIg", WantPics = true, TeamId = teamFem1A.Id };
            var playerF1B1 = new Player() { Id = 13, NIF = "12354678K", Name = "playerF2B1", Phone = "111222333", Instagram = "playerIg", WantPics = true, TeamId = teamFem1A.Id };
            var playerF1B3 = new Player() { Id = 14, NIF = "12354678ZL", Name = "playerF2B3", Phone = "111222333", Instagram = "playerIg", WantPics = true, TeamId = teamFem1A.Id };

            #endregion

            #region Games default values

            var game1 = new Game() { Id = 1, Team1Id = 1, Team2Id = 2, Score1 = 2, Score2 = 15, Court = 1, Schedule = new DateTime(), StaffId = 2 };
            var game2 = new Game() { Id = 2, Team1Id = 3, Team2Id = 1, Score1 = 21, Score2 = 1, Court = 2, Schedule = new DateTime(), StaffId = 2 };
            
            #endregion

            #region Seed data

            modelBuilder.Entity<Edition>().HasData(new Edition[] { edit1, edit2, edit3, edit4 });

            modelBuilder.Entity<Category>().HasData(new Category[] { catMasc, catFem, catMin });

            modelBuilder.Entity<User>().HasData(new User[] { admin, staff, user });
            
            // modelBuilder.Entity<Group>().HasData(new Group[] { groupMascA, groupMascB, groupMascC, groupFemA, groupFemB });

            // modelBuilder.Entity<Team>().HasData(new Team[]
            // {
            //     teamMasc1A, teamMasc2A, teamMasc3A,
            //     teamMasc1B, teamMasc2B, teamMasc3B,
            //     teamMasc1C, teamMasc2C, teamMasc3C,
            //     teamFem1A, teamFem2A,
            //     teamFem1B, teamFem2B
            // });
            
            // modelBuilder.Entity<Player>().HasData(new Player[]
            // {
            //     playerM1A1, playerM1A2, playerM1A3,
            //     playerM2A1, playerM2A2, playerM2A3,
            //     playerM3A1, playerM3A2, playerM3A3,
            //     playerF1A1, playerF1A2,
            //     playerF1B1, playerF1B3
            // });

            // modelBuilder.Entity<Game>().HasData(new Game[] { game1, game2 });

            #endregion
        }
    }
}
