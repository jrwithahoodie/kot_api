using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class TeamRequestInputDTO
    {
        public string Name { get; set; }
        public bool Pay { get; set; }
        public string CategoryName { get; set; }
        public string? EditionName { get; set; }
    }
}