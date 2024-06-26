using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Edition
{
    public interface IEditionBll
    {
        IEnumerable<Entities.Entities.Edition> Get();
        Entities.Entities.Edition Post(Entities.Entities.Edition newEditionData);
        Entities.Entities.Edition Update(string editionName);
    }
}