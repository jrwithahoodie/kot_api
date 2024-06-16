using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Mail { get; set; }

        public string Role { get; set; }

        [ForeignKey("PlayerId")]
        public int? PlayerId { get; set; }

        public Player? Player { get; set; }
    }
}
