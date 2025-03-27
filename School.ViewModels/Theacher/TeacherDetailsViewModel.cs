﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.ViewModels.Theacher
{
    public class TeacherDetailsViewModel
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<string> Images { get; set; }

    }
}
