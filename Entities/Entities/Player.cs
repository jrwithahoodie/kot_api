using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Index(nameof(NIF), IsUnique = true)]
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string? NIF { get; set; }

        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Instagram { get; set; }

        public bool WantPics { get; set; }

        [ForeignKey("TeamId")]
        public int TeamId { get; set; }

        public Team? Team { get; set; }

    }
}
