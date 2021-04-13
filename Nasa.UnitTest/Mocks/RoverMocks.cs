using System.Collections.Generic;
using Nasa.Model.Movements;

namespace Nasa.UnitTest.Mocks
{
    public class RoverMocks
    {
        public static readonly List<string> OkTest = new List<string>
        {
         "1 2 N|LMLMLMLMM",
         "3 3 E|MMRMMRMRRM"
        };
        public static readonly List<string> CrashTest = new List<string>
        {
            "1 2 N|LMLMLMLMM",
            "3 3 W|MMRMMRMRRM"
        };
    }
}
