using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class PlayerRequestInputDTO
    {
        public string? NIF {get; set;}
        public string Name {get; set;}
        public string Phone {get; set;}
        public string Instagram {get; set;}
        public bool WantPics {get; set;}
    }
}