using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class TeamUpdateRequestInputDTO
    {
        public string Name { get; set; }
        public bool Pay { get; set; }
        public int Wins { get; set; }
        public int Defeats { get; set; }
        public int Classification_points { get; set; }
        public int Points_diff { get; set; }
        public string EditionName { get; set; }
        public string CategoryName { get; set; }
    }
}