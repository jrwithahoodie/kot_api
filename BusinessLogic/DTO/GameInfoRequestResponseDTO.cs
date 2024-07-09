using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class GameInfoRequestResponseDTO
    {
        public int Id { get; set; }
        public string Team1Name { get; set; }
        public int Team1Score { get; set; }
        public string Team2Name { get; set; }
        public int Team2Score { get; set; }
        public int Court { get; set; }
    }
}