using EscapeMines.Abstractions.Commands;
using System.Collections.Generic;

namespace EscapeMines.Abstractions.Services
{
    public interface ICommandService
    {
        void InitBoardSetCommand(IBoard board, ICommand command);
        void InitMineSetCommand(IBoard board, IList<IMine> mines, ICommand command);
        void InitExitSetCommand(IBoard board, IExit exit, ICommand command);
        void InitTurtleSetCommand(IBoard board, ITurtle turtle, ICommand command);
        void InitTurtleMoveCommand(ITurtle turtle, IEnumerable<IMine> mines, IExit exit, ICommand command);
    }
}