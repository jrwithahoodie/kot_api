using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class NewTeamDTO
    {
        public string Name { get; set; }
        public int EditionId { get; set; }
        public int CategoryId { get; set; }
        public bool Payed { get; set; }

    }
}