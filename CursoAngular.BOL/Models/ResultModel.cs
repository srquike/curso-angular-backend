﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoAngular.BOL.Models
{
    public class ResultModel
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
}
