﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Review 
{ 
    public class ReviewViewDto
    {

        public string UserFullName { get; set; } = "";
        public string Text { get; set; } = "";
        public int Rating { get; set; }

    }
}
