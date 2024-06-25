using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DTO;

namespace BusinessLogic.Player
{
    public interface IPlayerBll
    {
        IEnumerable<PlayerRequestResponseDTO> Get();
        Entities.Entities.Player Get(string nif);
        Entities.Entities.Player Post(PlayerRequestInputDTO value);
        IEnumerable<Entities.Entities.Player> PostSeveralPlayers(IEnumerable<Entities.Entities.Player> value);
    }
}
