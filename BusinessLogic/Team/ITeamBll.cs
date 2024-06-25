using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DTO;

namespace BusinessLogic.Team
{
    public interface ITeamBll
    {
        IEnumerable<TeamRequestResponseDTO> Get();
        TeamRequestResponseDTO Get(string name);
        IEnumerable<TeamRequestResponseDTO> GetClassif(string groupName);
        IEnumerable<TeamRequestResponseDTO> GetByGroup(string groupName);
        TeamRequestResponseDTO Post(TeamRequestInputDTO newTeamData);
    }
}
