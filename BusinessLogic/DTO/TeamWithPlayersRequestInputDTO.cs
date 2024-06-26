using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class TeamWithPlayersRequestInputDTO
    {
        public string Name { get; set; }
        public bool Pay { get; set; }
        public string CategoryName { get; set; }
        public List<PlayerInTeamRequestInputDTO> PlayerList { get; set; }
    }
}