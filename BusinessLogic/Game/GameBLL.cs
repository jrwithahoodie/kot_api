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
                .ThenInclude(t => t.Category)
                .Include(g => g.Team1)
                .ThenInclude(t => t.Edition)
                .Include(g => g.Team2)
                .ThenInclude(t => t.Category)
                .Include(g => g.Team2)
                .ThenInclude(t => t.Edition)
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
                .ThenInclude(t => t.Category)
                .Include(g => g.Team1)
                .ThenInclude(t => t.Edition)
                .Include(g => g.Team2)
                .ThenInclude(t => t.Category)
                .Include(g => g.Team2)
                .ThenInclude(t => t.Edition)
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
                    .ThenInclude(t => t.Category)
                    .Include(g => g.Team1)
                    .ThenInclude(t => t.Edition)
                    .Include(g => g.Team2)
                    .ThenInclude(t => t.Category)
                    .Include(g => g.Team2)
                    .ThenInclude(t => t.Edition)
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
                    .ThenInclude(t => t.Category)
                    .Include(g => g.Team1)
                    .ThenInclude(t => t.Edition)
                    .Include(g => g.Team2)
                    .ThenInclude(t => t.Category)
                    .Include(g => g.Team2)
                    .ThenInclude(t => t.Edition)
                    .Where(g => g.Court == court)
                    .OrderBy(g => g.Schedule)
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

        public IEnumerable<GameInfoRequestResponseDTO> GetByGroup(string groupName, string editionName)
        {
            try
            {
                var existingGroup = _context.Groups
                    .Where(g => g.Name == groupName)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception($"No existe ningún grupo '{groupName}'");
                
                var existingEdition =_context.Editions
                    .Where(e => e.Name == editionName)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception($"No existe ninguna edición {editionName}");

                var gameList = _context.Games
                    .Include(g => g.Team1)
                        .ThenInclude(t => t.Category)
                    .Include(g => g.Team1)
                        .ThenInclude(t => t.Edition)
                    .Include(g => g.Team2)
                        .ThenInclude(t => t.Category)
                    .Include(g => g.Team2)
                        .ThenInclude(t => t.Edition)
                    .Where(g => (g.Team1.Group.Name == groupName && g.Team1.Edition.Name == editionName) ||
                                (g.Team2.Group.Name == groupName && g.Team2.Edition.Name == editionName))
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

        public IEnumerable<GameInfoRequestResponseDTO> GetByTeam(string teamName, string editionName)
        {
            try
            {
                var existingTeam = _context.Teams
                    .Where(g => g.Name == teamName)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception($"No existe ningún equipo '{teamName}'");
                
                var existingEdition =_context.Editions
                    .Where(e => e.Name == editionName)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception($"No existe ninguna edición {editionName}");

                var gameList = _context.Games
                    .Include(g => g.Team1)
                        .ThenInclude(t => t.Category)
                    .Include(g => g.Team1)
                        .ThenInclude(t => t.Edition)
                    .Include(g => g.Team2)
                        .ThenInclude(t => t.Category)
                    .Include(g => g.Team2)
                        .ThenInclude(t => t.Edition)
                    .Where(g => (g.Team1.Name == teamName && g.Team1.Edition.Name == editionName) ||
                                (g.Team2.Name == teamName && g.Team2.Edition.Name == editionName))
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

                newGame.Schedule = newGameData.Schedule;
                newGame.StaffId = 2;

                var result = _context.Games.Add(newGame);
                _context.SaveChanges();

                var resultMapped = _mapper.Map<GameInfoRequestResponseDTO>(result.Entity);
                resultMapped.Team1 = _mapper.Map<TeamRequestResponseDTO>(existingTeam1);
                resultMapped.Team2 = _mapper.Map<TeamRequestResponseDTO>(existingTeam2);

                return resultMapped;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

        public GameInfoRequestResponseDTO AlterGameResult(AlterGameResultRequestDTO gameResultInfo)
        {
            try
            {
                var existingTeam1 =_context.Teams
                    .Where(t => t.Name == gameResultInfo.Team1Name)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception($"EL equipo 1 con nombre '{gameResultInfo.Team1Name}' no existe");
                
                var existingTeam2 =_context.Teams
                    .Where(t => t.Name == gameResultInfo.Team2Name)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception($"EL equipo 1 con nombre '{gameResultInfo.Team2Name}' no existe");

                var existingGame = _context.Games
                    .Include(g => g.Team1)
                    .Include(g => g.Team2)
                    .ThenInclude(t => t.Category)
                    .Include(g => g.Team1)
                    .ThenInclude(t => t.Edition)
                    .Include(g => g.Team2)
                    .ThenInclude(t => t.Category)
                    .Include(g => g.Team2)
                    .ThenInclude(t => t.Edition)
                    .Where(g => g.Team1Id == existingTeam1.Id && g.Team2Id == existingTeam2.Id)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception($"No existe ningún partido con esos datos.'");

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

        public GameInfoRequestResponseDTO AlterGameInfo(AlterGameInfoRequestDTO gameNewInfo)
        {
            try
            {
                var existingGame = _context.Games
                    .Include(g => g.Team1)
                    .Include(g => g.Team2)
                    .ThenInclude(t => t.Category)
                    .Include(g => g.Team1)
                    .ThenInclude(t => t.Edition)
                    .Include(g => g.Team2)
                    .ThenInclude(t => t.Category)
                    .Include(g => g.Team2)
                    .ThenInclude(t => t.Edition)
                    .Where(g => g.Id == gameNewInfo.GameId)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception($"No existe ningún partido con estas características");
                
                if (existingGame.Schedule != gameNewInfo.NewSchedule)
                {
                    existingGame.Schedule = gameNewInfo.NewSchedule;
                }

                if (existingGame.Court !=  gameNewInfo.NewCourt)
                {
                    existingGame.Court = gameNewInfo.NewCourt;
                }

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
