﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
