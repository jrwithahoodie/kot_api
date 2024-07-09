using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class GameInfoRequestResponseDTO
    {
        public int Id { get; set; }
        public TeamRequestResponseDTO Team1 { get; set; }
        public int Team1Score { get; set; }
        public TeamRequestResponseDTO Team2 { get; set; }
        public int Team2Score { get; set; }
        public int Court { get; set; }
    }
}