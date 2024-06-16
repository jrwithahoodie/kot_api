using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Category { get; set; }

        public bool Pay { get; set; }

        public int Wins { get; set; }

        public int Defeats { get; set; }

        public int Points_diff { get; set; }

        public int Classification_points { get; set;}

        [ForeignKey("GroupId")]
        public int GroupId { get; set; }

        public Group? Group { get; set; }
    }
}
