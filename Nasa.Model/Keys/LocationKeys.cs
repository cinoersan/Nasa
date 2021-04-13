using System.Collections.Generic;
using Nasa.Model.Movements;

namespace Nasa.Model.Keys
{
    public class LocationKeys
    {
        public static readonly List<string> Directions = new List<string>() { "N", "E", "S", "W" };
        public static readonly Increment GoNorth = new Increment { IncrementX = 0, IncrementY = 1 };
        public static readonly Increment GoEast = new Increment { IncrementX = 1, IncrementY = 0 };
        public static readonly Increment GoSouth = new Increment { IncrementX = 0, IncrementY = -1 };
        public static readonly Increment GoWest = new Increment { IncrementX = -1, IncrementY = 0 };
        public static readonly List<Increment> Increments = new List<Increment>() { GoNorth, GoEast, GoSouth, GoWest };
        public static readonly Movement TurnRight = new Movement {Key = "R", Increment = 1};
        public static readonly Movement TurnLeft = new Movement { Key = "L", Increment = -1 };
        public static readonly List<Movement> Movements = new List<Movement> { TurnLeft, TurnRight };
        public const string Move = "M";
        public const string MovementRegex = @"^(\d+\s?)(\d+\s?)([NESW]\s?)\|([LRM]+)$";
        public const string PlateauRegex = @"^\((\s?\d+\s?),(\s?\d+\s?)\)$";
    }
}
