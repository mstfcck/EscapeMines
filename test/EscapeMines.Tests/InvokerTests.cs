using EscapeMines.Abstractions;
using EscapeMines.Abstractions.Services;
using EscapeMines.BoardObjects;
using EscapeMines.Exceptions;
using EscapeMines.Services;
using EscapeMines.TurtleObjects;
using EscapeMines.Validators;
using Moq;
using System.Text;
using Xunit;

namespace EscapeMines.Tests
{
    public class InvokerTests
    {
        private readonly ICommandParserService _commandParserService;
        private readonly IInvoker _invoker;

        public InvokerTests()
        {
            IInstructionValidator instructionValidator = Mock.Of<InstructionValidator>();
            _commandParserService = new CommandParserService(instructionValidator);
            ICommandService commandService = new CommandService();
            ITurtleMoveService turtleMoveService = Mock.Of<TurtleMoveService>();
            IBoard board = Mock.Of<Board>();
            IExit exit = Mock.Of<Exit>();
            ITurtle turtle = new Turtle(turtleMoveService);
            _invoker = new Invoker(board, exit, turtle, commandService);
        }

        [Theory]
        [InlineData("5 4", "1,1 1,3 3,3", "4 2", "0 1 N", "R M L M M", "Mine Hit", "1 1 East")]
        [InlineData("5 4", "1,0 1,3 3,3", "2 1", "0 1 N", "R M M", "Success", "2 1 East")]
        [InlineData("5 4", "1,0 1,3 3,3", "5 1", "0 1 N", "R M M", "Still In Danger", "2 1 East")]
        public void Invoker_Valid_Tests(string boardSize, string listOfMines, string exitPoint, string turtleStartingPoint, string movements, string result, string latestLocation)
        {
            var input = new StringBuilder();
            input.AppendLine(boardSize);
            input.AppendLine(listOfMines);
            input.AppendLine(exitPoint);
            input.AppendLine(turtleStartingPoint);
            input.Append(movements);

            var commands = _commandParserService.Parse(input.ToString());
            _invoker.SetCommands(commands);
            _invoker.InvokeCommands();

            var commandResult = _invoker.GetResult();
            var commandLatestLocation = _invoker.GetLatestTurtleLocation();

            Assert.Equal(result, commandResult);
            Assert.Equal(latestLocation, commandLatestLocation);
        }

        [Fact]
        public void Invoker_Parser_Is_Not_Valid_Tests()
        {
            Assert.Throws<InstructionValidationException>(() => _commandParserService.Parse("INVALID_INSTRUCTIONS"));
        }
    }
}
