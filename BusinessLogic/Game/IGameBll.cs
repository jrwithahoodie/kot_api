using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Game
{
    public interface IGameBll
    {
        IEnumerable<Entities.Entities.Game> Get();
        Entities.Entities.Game Get(int id);
        IEnumerable<Entities.Entities.Game> GetByStaff(int staffId);
        IEnumerable<Entities.Entities.Game> GetByCourt(int id);
        Entities.Entities.Game Post(Entities.Entities.Game value);
        Entities.Entities.Game Put(int id, int score1, int score2);
        Entities.Entities.Game Delete(int id);
    }
}
