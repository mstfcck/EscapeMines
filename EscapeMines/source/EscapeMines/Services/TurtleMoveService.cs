using EscapeMines.Abstractions;
using EscapeMines.Abstractions.Services;
using EscapeMines.TurtleObjects;
using System.Collections.Generic;
using System.Linq;

namespace EscapeMines.Services
{
    public class TurtleMoveService : ITurtleMoveService
    {
        public bool IsMineHit(ILocation location, IEnumerable<IMine> mines)
        {
            return mines.Any(x => x.Location.X == location.X && x.Location.Y == location.Y);
        }

        public bool IsExit(ILocation location, IExit exit)
        {
            return exit.Location.X == location.X && exit.Location.Y == location.Y;
        }

        public void MoveForward(TurtleLocation location)
        {
            switch (location.CardinalPoint)
            {
                case CardinalPoint.North:
                    location.Y += 1;
                    break;
                case CardinalPoint.West:
                    location.X -= 1;
                    break;
                case CardinalPoint.South:
                    location.Y -= 1;
                    break;
                case CardinalPoint.East:
                    location.X += 1;
                    break;
            }
        }

        public void MoveLeft(TurtleLocation location)
        {
            switch (location.CardinalPoint)
            {
                case CardinalPoint.North:
                    location.CardinalPoint = CardinalPoint.West;
                    break;
                case CardinalPoint.West:
                    location.CardinalPoint = CardinalPoint.South;
                    break;
                case CardinalPoint.South:
                    location.CardinalPoint = CardinalPoint.East;
                    break;
                case CardinalPoint.East:
                    location.CardinalPoint = CardinalPoint.North;
                    break;
            }
        }

        public void MoveRight(TurtleLocation location)
        {
            switch (location.CardinalPoint)
            {
                case CardinalPoint.North:
                    location.CardinalPoint = CardinalPoint.East;
                    break;
                case CardinalPoint.East:
                    location.CardinalPoint = CardinalPoint.South;
                    break;
                case CardinalPoint.South:
                    location.CardinalPoint = CardinalPoint.West;
                    break;
                case CardinalPoint.West:
                    location.CardinalPoint = CardinalPoint.North;
                    break;
            }
        }
    }
}
