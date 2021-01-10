using EscapeMines.Abstractions;
using EscapeMines.Abstractions.Commands;
using EscapeMines.Abstractions.Services;
using EscapeMines.BoardObjects;
using System.Collections.Generic;

namespace EscapeMines.Services
{
    public class CommandService : ICommandService
    {
        public void InitBoardSetCommand(IBoard board, ICommand command)
        {
            var c = (IBoardSetCommand)command;
            c.Set(board);
        }

        public void InitMineSetCommand(IBoard board, IList<IMine> mines, ICommand command)
        {
            var mine = new Mine();
            mines.Add(mine);
            var mineSetCommand = (IMineSetCommand)command;
            mineSetCommand.Set(board, mine);
        }

        public void InitExitSetCommand(IBoard board, IExit exit, ICommand command)
        {
            var exitSetCommand = (IExitSetCommand)command;
            exitSetCommand.Set(board, exit);
        }

        public void InitTurtleSetCommand(IBoard board, ITurtle turtle, ICommand command)
        {
            var turtleSetCommand = (ITurtleSetCommand)command;
            turtleSetCommand.Set(board, turtle);
        }

        public void InitTurtleMoveCommand(ITurtle turtle, IEnumerable<IMine> mines, IExit exit, ICommand command)
        {
            var turtleMoveCommand = (ITurtleMoveCommand)command;
            turtleMoveCommand.Set(turtle, mines, exit);
        }
    }
}
