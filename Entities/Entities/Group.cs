﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
