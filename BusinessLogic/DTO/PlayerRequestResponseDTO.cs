using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class PlayerRequestResponseDTO
    {
        public int Id { get; set; }
        public string NIF { get; set; }
        public string Name {get; set; }
        public string Phone { get; set; }
        public string Instagram { get; set; }
        public bool WantPics { get; set; }
        public string TeamName { get; set; }
        public string EditionName { get; set; }
        public string CategoryName { get; set; }
    }
}