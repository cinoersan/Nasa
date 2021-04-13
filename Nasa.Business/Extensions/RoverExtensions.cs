using Nasa.Model.Keys;
using Nasa.Model.Movements;
using System.Linq;

namespace Nasa.Business.Extensions
{
    public static class RoverExtensions
    {
        public static void Move(this RoverStatus status, string movement)
        {
            if (movement.Equals(LocationKeys.Move))
                status.MoveForward();
            else
                status.Turn(movement);
        }

        private static void Turn(this RoverStatus status, string movementKey)
        {
            var movement = LocationKeys.Movements.FirstOrDefault(t => t.Key.Equals(movementKey));
            var index = LocationKeys.Directions.FindIndex(t => t.Equals(status.Heading)) + (movement?.Increment ?? 0);
            status.Heading = LocationKeys.Directions[(index + 4) % 4];
        }
        private static void MoveForward(this RoverStatus status)
        {
            var index = LocationKeys.Directions.FindIndex(t => t.Equals(status.Heading));
            var movement = LocationKeys.Increments[index];
            status.CurrentCoordinate.X += movement.IncrementX;
            status.CurrentCoordinate.Y += movement.IncrementY;
        }
    }
}
