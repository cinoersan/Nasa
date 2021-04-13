using System.Collections.Generic;

namespace Nasa.Model.Movements
{
    public class RoverStatus
    {
        public string Heading { get; set; }
        public string MovementText { get; set; }
        public Coordinate CurrentCoordinate { get; set; } = new Coordinate();
        public List<Coordinate> Coordinates { get; set; } = new List<Coordinate>();
    }
}
