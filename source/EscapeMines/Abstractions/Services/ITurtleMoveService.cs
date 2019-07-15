using EscapeMines.TurtleObjects;
using System.Collections.Generic;

namespace EscapeMines.Abstractions.Services
{
    public interface ITurtleMoveService
    {
        bool IsMineHit(ILocation location, IEnumerable<IMine> mines);
        bool IsExit(ILocation location, IExit exit);
        void MoveForward(TurtleLocation location);
        void MoveLeft(TurtleLocation location);
        void MoveRight(TurtleLocation location);
    }
}