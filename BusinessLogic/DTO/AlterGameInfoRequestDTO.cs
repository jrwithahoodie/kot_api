using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class AlterGameInfoRequestDTO
    {
        public int GameId { get; set; }
        public DateTime NewSchedule { get; set; }
        public int NewCourt { get; set; }
    }
}