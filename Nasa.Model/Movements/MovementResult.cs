using System;
using System.Collections.Generic;
using System.Text;
using Nasa.Model.Config;

namespace Nasa.Model.Movements
{
    public class MovementResult
    {
        public Plateau Plateau { get; set; }
        public List<RoverStatus> Rovers { get; set; }
    }
}
