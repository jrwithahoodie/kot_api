using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class TeamRequestResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Pay { get; set; }
        public int Wins { get; set; }
        public int Defeats { get; set; }
        public int Classification_points { get; set; }
        public string EditionName { get; set; }
        public string CategoryName { get; set; }
    }
}