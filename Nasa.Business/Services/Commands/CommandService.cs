using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Nasa.Business.Exceptions;
using Nasa.Business.Extensions;
using Nasa.Business.Repositories.Configuration;
using Nasa.Business.Repositories.Movements;
using Nasa.Model.Config;
using Nasa.Model.Keys;
using Nasa.Model.Movements;

namespace Nasa.Business.Services.Commands
{
    public class CommandService : ICommandService
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IMapper _mapper;
        public CommandService(IConfigurationRepository configurationRepository,
            IMapper mapper)
        {
            _configurationRepository = configurationRepository;
            _mapper = mapper;
        }

        public async Task<MovementResult> MoveAll(List<string> movements)
        {
            var list = new List<RoverStatus>();
            // Movements are those gathered from the file
            for (var i = 0; i < movements.Count; i++)
            {
                // First record can be plateau setup, verifies pattern via regex;,
                if (i.Equals(0))
                {
                    var plateauMatch = Regex.Match(movements[i], LocationKeys.PlateauRegex);
                    if (plateauMatch.Success)
                    {
                        await SavePlateau(plateauMatch);
                        continue;
                    }
                }

                // If it is not first record it should be movement and should match movement regex pattern;
                var movementMatch = Regex.Match(movements[i], LocationKeys.MovementRegex);
                if (!movementMatch.Success)
                    throw new BadRequestException($"Movement line {i + 1} is not valid");

                // gets the initial values
                var status = GetInitialStatus(movementMatch);

                // add to the list for front end
                list.Add(status);
            }
            // Check if there are more than 1 rover in the same location

            var counted = list
                .GroupBy(t => new { t.CurrentCoordinate.X, t.CurrentCoordinate.Y })
                .Select(t => new
                {
                    Coordinate = $"{t.Key.X}-{t.Key.Y}",
                    Count = t.Count()
                });

            if (counted.Any(t => t.Count > 1))
                throw new BadRequestException("There are rovers at the same location");

            // Moving the rovers and checking if it crashes to another one.
            var result = await MoveRovers(list);

            return result;

        }
        private async Task SavePlateau(Match plateauMatch)
        {

            var plateau = new Plateau
            {
                X = plateauMatch.Groups[1].Value.ToNullableInt() ?? 5,
                Y = plateauMatch.Groups[1].Value.ToNullableInt() ?? 5,
            };

            await _configurationRepository.SetConfig(plateau, ConfigKeys.Plateau);

        }
        private static RoverStatus GetInitialStatus(Match movement)
        {
            return new RoverStatus
            {
                Heading = movement.Groups[3].Value.Trim(),
                CurrentCoordinate = new Coordinate
                {
                    X = movement.Groups[1].Value.Trim().ToNullableInt() ?? 0,
                    Y = movement.Groups[2].Value.Trim().ToNullableInt() ?? 0
                },
                MovementText = movement.Groups[4].Value
            };
        }
        private async Task<MovementResult> MoveRovers(List<RoverStatus> list)
        {
            var topRight = await _configurationRepository.GetConfig<Plateau>(ConfigKeys.Plateau);
            foreach (var rover in list)
            {
                var otherRovers = list.Where(t =>
                        t.CurrentCoordinate.X != rover.CurrentCoordinate.X ||
                        t.CurrentCoordinate.Y != rover.CurrentCoordinate.Y)
                    .Select(t => t.CurrentCoordinate).ToList();
                MoveSingle(rover, otherRovers);

                //Check if rover is supposed go out of Plateau
                if (rover.Coordinates.Any(t => t.X > topRight.X || t.Y > topRight.Y)
                    || rover.Coordinates.Any(t => t.X < 0 || t.Y < 0))
                    throw new BadRequestException("The list leads some rovers out of the plateau");
            }
            
            return new MovementResult
            {
                Rovers = list,
                Plateau = topRight
            };
        }
        private void MoveSingle(RoverStatus rover, List<Coordinate> otherRovers)
        {
            var list = new List<Coordinate>();
            list.Add(_mapper.Map<Coordinate>(rover));
            foreach (var mov in rover.MovementText)
            {
                // rover moves and check if there is any other rover in its way.
                rover.Move(mov.ToString());
                if (otherRovers.Any(t => t.X == rover.CurrentCoordinate.X &&
                                          t.Y == rover.CurrentCoordinate.Y))
                    throw new BadRequestException($"With this mapping there will be crash at coordinate " +
                                                  $"(${rover.CurrentCoordinate.X},  ${rover.CurrentCoordinate.Y})");
                if (!mov.ToString().Equals(LocationKeys.Move)) continue;
                var item = _mapper.Map<Coordinate>(rover);
                list.Add(item);
            }
            rover.Coordinates = list;
        }
    }
}
