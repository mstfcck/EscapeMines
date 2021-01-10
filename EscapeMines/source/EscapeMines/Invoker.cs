using EscapeMines.Abstractions;
using EscapeMines.Abstractions.Commands;
using EscapeMines.Abstractions.Services;
using System.Collections.Generic;

namespace EscapeMines
{
    public class Invoker : IInvoker
    {
        private readonly IBoard _board;
        private readonly IList<IMine> _mines;
        private readonly IExit _exit;
        private readonly ITurtle _turtle;
        private readonly ICommandService _commandService;
        private IEnumerable<ICommand> _commands;

        public Invoker(IBoard board, IExit exit, ITurtle turtle, ICommandService commandService)
        {
            _board = board;
            _mines = new List<IMine>();
            _exit = exit;
            _turtle = turtle;
            _commandService = commandService;
        }

        public void SetCommands(IEnumerable<ICommand> commands)
        {
            _commands = commands;
        }

        public void InvokeCommands()
        {
            foreach (var command in _commands)
            {
                var name = command.GetType().GetInterfaces()[0].Name;
                switch (name)
                {
                    case nameof(IBoardSetCommand):
                        _commandService.InitBoardSetCommand(_board, command);
                        break;

                    case nameof(IMineSetCommand):
                        _commandService.InitMineSetCommand(_board, _mines, command);
                        break;

                    case nameof(IExitSetCommand):
                        _commandService.InitExitSetCommand(_board, _exit, command);
                        break;

                    case nameof(ITurtleSetCommand):
                        _commandService.InitTurtleSetCommand(_board, _turtle, command);
                        break;

                    case nameof(ITurtleMoveCommand):
                        _commandService.InitTurtleMoveCommand(_turtle, _mines, _exit, command);
                        break;
                }
                command.Execute();
            }
        }

        public string GetLatestTurtleLocation()
        {
            return $"{_turtle.Location.X} {_turtle.Location.Y} {_turtle.Location.CardinalPoint}";
        }

        public string GetResult()
        {
            return _turtle.Result;
        }
    }
}