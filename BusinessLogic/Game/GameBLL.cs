using Entities.AppContext;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Game
{
    public class GameBLL : IGameBll
    {
        #region Fields

        private readonly Context _context;

        #endregion

        #region Builder

        public GameBLL()
        {
            _context = new Context();
        }

        #endregion

        public Entities.Entities.Game Delete(int id)
        {
            try
            {
                var existingGame = _context.Games
                    .Where(g => g.Id == id)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception($"No existe ningún partido con id '{id}'");
                
                _context.Games.Remove(existingGame);
                _context.SaveChanges();

                return existingGame;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

        public IEnumerable<Entities.Entities.Game> Get()
        {
            var gameList = _context.Games.ToList();

            return gameList;
        }

        public Entities.Entities.Game Get(int id)
        {
            var user = _context.Games.Where(g => g.Id == id).ToList().FirstOrDefault();

            return user;
        }

        public IEnumerable<Entities.Entities.Game> GetByStaff(int staffId)
        {
            try
            {
                var gameList = _context.Games
                .Include(g => g.Team1)
                .Include(g => g.Team2)
                .Where(g => g.StaffId == staffId).ToList();

                return gameList;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }            
        }

        public IEnumerable<Entities.Entities.Game> GetByCourt(int court)
        {
            try
            {
                var gameList = _context.Games
                    .Include(g => g.Team1)
                    .Include(g => g.Team2)
                    .Where(g => g.Court == court).ToList();

                return gameList;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }            
        }

        public Entities.Entities.Game Post(Entities.Entities.Game value)
        {
            var team1_db = _context.Teams.Where(g => g.Id == value.Team1Id).FirstOrDefault();
            var team2_db = _context.Teams.Where(g => g.Id == value.Team2Id).FirstOrDefault();

            var staff = _context.Users.Where(s => s.Id == value.StaffId).FirstOrDefault();

            var auxGame = new Entities.Entities.Game
            {
                Team1 = team1_db,
                Team2 = team2_db,
                Staff = staff,
                Schedule = value.Schedule,
                Court = value.Court,
                Score1 = value.Score1,
                Score2 = value.Score2,
                Score1Old = 0,
                Score2Old = 0,
                Team1Id = team1_db.Id,
                Team2Id = team2_db.Id,
                StaffId = staff.Id
            };

            var result = _context.Games.Add(auxGame);

            _context.SaveChanges();

            return result.Entity;
        }

        public Entities.Entities.Game Put(int id, int score1, int score2)
        {
            try
            {
                // recuperar el partido que tiene id = id
                var game = _context.Games
                    .Include(t => t.Team1)
                    .Include(t => t.Team2)
                    .Where(g => g.Id == id).ToList().FirstOrDefault();
                // actualizar los campos de score old con los valoes score que traifo de db
                var score1old = game.Score1;
                var score2old = game.Score2;

                game.Score1Old = score1old;
                game.Score2Old = score2old;
                // actualizar los valores normales con los datos nuevos
                game.Score1 = score1;
                game.Score2 = score2;
                // hacer un update y savechanges
                _context.Update(game);
                _context.SaveChanges();

                return game;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }
    }
}
