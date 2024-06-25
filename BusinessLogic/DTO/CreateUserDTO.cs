using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class CreateUserDTO
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Role { get; set; }
    }
}