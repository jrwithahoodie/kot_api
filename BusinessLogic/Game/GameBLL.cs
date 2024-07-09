using AutoMapper;
using BusinessLogic.DTO;
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
        private readonly IMapper _mapper;

        #endregion

        #region Builder

        public GameBLL(IMapper mapper)
        {
            _context = new Context();
            _mapper = mapper;
        }

        #endregion

        public IEnumerable<GameInfoRequestResponseDTO> GetAllGames()
        {
            var gameList = _context.Games
            .Include(g => g.Team1)
            .Include(g => g.Team2)
            .ToList();

            var response = _mapper.Map<IEnumerable<GameInfoRequestResponseDTO>>(gameList);
            
            return response;
        }

        public GameInfoRequestResponseDTO GetGame(int gameId)
        {
            try
            {
                var existingGame = _context.Games
                .Include(g => g.Team1)
                .Include(g => g.Team2)
                .Where(g => g.Id == gameId)
                .ToList()
                .FirstOrDefault()
                    ?? throw new Exception($"No existe ningún partido con el id '{gameId}'");

                var mappedGame = _mapper.Map<GameInfoRequestResponseDTO>(existingGame);

                return mappedGame;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

        public IEnumerable<GameInfoRequestResponseDTO> GetByStaff(int staffId)
        {
            try
            {
                var gameList = _context.Games
                    .Include(g => g.Team1)
                    .Include(g => g.Team2)
                    .Where(g => g.StaffId == staffId)
                    .ToList();

                var gameListMapped = _mapper.Map<IEnumerable<GameInfoRequestResponseDTO>>(gameList);
                
                return gameListMapped;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }            
        }

        public IEnumerable<GameInfoRequestResponseDTO> GetByCourt(int court)
        {
            try
            {
                var gameList = _context.Games
                    .Include(g => g.Team1)
                    .Include(g => g.Team2)
                    .Where(g => g.Court == court)
                    .ToList();

                var gameListMapped = _mapper.Map<IEnumerable<GameInfoRequestResponseDTO>>(gameList);
                
                return gameListMapped;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }            
        }

        public GameInfoRequestResponseDTO Post(NewGameRequestDTO newGameData)
        {
            try
            {
                var existingTeam1 = _context.Teams
                .Where(t => t.Name == newGameData.Team1Name)
                .ToList()
                .FirstOrDefault()
                    ?? throw new Exception($"No hay ningún equipo con nombre '{newGameData.Team1Name}'");
            
                var existingTeam2 = _context.Teams
                    .Where(t => t.Name == newGameData.Team2Name)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception($"No hay ningún equipo con nombre '{newGameData.Team2Name}'");

                var newGame = _mapper.Map<Entities.Entities.Game>(newGameData);

                newGame.Team1Id = existingTeam1.Id;
                newGame.Team2Id = existingTeam2.Id;

                newGame.Schedule = DateTime.Now;
                newGame.StaffId = 2;

                var result = _context.Games.Add(newGame);
                _context.SaveChanges();

                var resultMapped = _mapper.Map<GameInfoRequestResponseDTO>(result.Entity);
                resultMapped.Team1Name = existingTeam1.Name;
                resultMapped.Team2Name = existingTeam2.Name;

                return resultMapped;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

        public GameInfoRequestResponseDTO Put(AlterGameResultRequestDTO gameResultInfo)
        {
            try
            {
                var existingGame = _context.Games
                    .Include(t => t.Team1)
                    .Include(t => t.Team2)
                    .Where(g => g.Id == gameResultInfo.GameId)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception($"No existe ningún partido con id '{gameResultInfo.GameId}'");

                var score1old = existingGame.Score1;
                var score2old = existingGame.Score2;

                existingGame.Score1Old = score1old;
                existingGame.Score2Old = score2old;
                
                existingGame.Score1 = gameResultInfo.Team1Score;
                existingGame.Score2 = gameResultInfo.Team2Score;
                
                _context.Update(existingGame);
                _context.SaveChanges();


                var gameMapped = _mapper.Map<GameInfoRequestResponseDTO>(existingGame);

                return gameMapped;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

        public GameInfoRequestResponseDTO Delete(int id)
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

                var gameMapped = _mapper.Map<GameInfoRequestResponseDTO>(existingGame);

                return gameMapped;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

    }
}
