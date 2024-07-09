using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class AllGroupsClassificationResponseDTO
    {
        public string GroupName { get; set; }
        public IEnumerable<TeamRequestResponseDTO> GroupTeams { get; set; }
    }
}