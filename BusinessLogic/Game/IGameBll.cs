using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DTO;

namespace BusinessLogic.Game
{
    public interface IGameBll
    {
        IEnumerable<GameInfoRequestResponseDTO> GetAllGames();
        GameInfoRequestResponseDTO GetGame(int id);
        IEnumerable<GameInfoRequestResponseDTO> GetByStaff(int staffId);
        IEnumerable<GameInfoRequestResponseDTO> GetByCourt(int id);
        GameInfoRequestResponseDTO Post(NewGameRequestDTO newGameData);
        GameInfoRequestResponseDTO Put(AlterGameResultRequestDTO gameResultInfo);
        GameInfoRequestResponseDTO Delete(int id);
    }
}
