﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntAICompetition.Models;

namespace AntAICompetition.Server
{
    public class Hill : ICoordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Owner { get; set; }
    }
}