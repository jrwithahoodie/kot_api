using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class NewGameRequestDTO
    {
        public string Team1Name { get; set; }
        public string Team2Name { get; set;}
        public int Court { get; set; }
        public DateTime Schedule { get; set; }
    }
}