using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Player
{
    public interface IPlayerBll
    {
        IEnumerable<Entities.Entities.Player> Get();
        Entities.Entities.Player Get(string nif);
        Entities.Entities.Player Post(Entities.Entities.Player value);
        IEnumerable<Entities.Entities.Player> PostSeveralPlayers(IEnumerable<Entities.Entities.Player> value);
        void Put(int id, string value);
        void Delete(string nif);
    }
}
