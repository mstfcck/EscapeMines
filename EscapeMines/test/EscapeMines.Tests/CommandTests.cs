using EscapeMines.Abstractions;
using EscapeMines.Abstractions.Commands;
using EscapeMines.Abstractions.Services;
using EscapeMines.BoardObjects;
using EscapeMines.Commands;
using EscapeMines.Common;
using EscapeMines.Exceptions;
using EscapeMines.Services;
using EscapeMines.TurtleObjects;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace EscapeMines.Tests
{
    public class BoardSetCommandTests
    {
        private readonly IBoard _board;
        private readonly IMine _mine;
        private readonly IExit _exit;
        private readonly ITurtle _turtle;
        private IBoardSetCommand _boardSetCommand;
        private IExitSetCommand _exitSetCommand;
        private IMineSetCommand _mineSetCommand;
        private ITurtleSetCommand _turtleSetCommand;
        private ITurtleMoveCommand _turtleMoveCommand;

        public BoardSetCommandTests()
        {
            ITurtleMoveService turtleMoveService = Mock.Of<TurtleMoveService>();
            _board = Mock.Of<Board>();
            _mine = Mock.Of<Mine>();
            _exit = Mock.Of<Exit>();
            _turtle = new Turtle(turtleMoveService);
        }

        [Fact]
        public void Board_Size_Should_Be_Same_With_Command_Size()
        {
            _boardSetCommand = new BoardSetCommand(new Size(5, 4));
            _boardSetCommand.Set(_board);
            _boardSetCommand.Execute();

            Assert.Equal(5, _board.Size.MaxX);
            Assert.Equal(4, _board.Size.MaxY);
        }

        [Fact]
        public void Exit_Set_Command_Returns_Location_Tests()
        {
            _boardSetCommand = new BoardSetCommand(new Size(5, 4));
            _boardSetCommand.Set(_board);
            _boardSetCommand.Execute();

            _exitSetCommand = new ExitSetCommand(new Location(3, 3));
            _exitSetCommand.Set(_board, _exit);
            _exitSetCommand.Execute();

            Assert.Equal(3, _exit.Location.X);
            Assert.Equal(3, _exit.Location.Y);
        }

        [Theory]
        [InlineData(6, 1)]
        [InlineData(0, 6)]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        public void Exit_Set_Command_Returns_Exception_Tests(int locationX, int locationY)
        {
            _boardSetCommand = new BoardSetCommand(new Size(5, 5));
            _boardSetCommand.Set(_board);
            _boardSetCommand.Execute();

            _exitSetCommand = new ExitSetCommand(new Location(locationX, locationY));
            _exitSetCommand.Set(_board, _exit);

            Assert.Throws<LocationValidationException>(() => _exitSetCommand.Execute());
        }

        [Fact]
        public void Mine_Set_Command_Returns_Location_Tests()
        {
            _boardSetCommand = new BoardSetCommand(new Size(5, 4));
            _boardSetCommand.Set(_board);
            _boardSetCommand.Execute();

            _mineSetCommand = new MineSetCommand(new Location(1, 2));
            _mineSetCommand.Set(_board, _mine);
            _mineSetCommand.Execute();

            Assert.Equal(1, _mine.Location.X);
            Assert.Equal(2, _mine.Location.Y);
        }

        [Theory]
        [InlineData(6, 1)]
        [InlineData(0, 6)]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        public void Mine_Set_Command_Returns_Exception_Tests(int locationX, int locationY)
        {
            _boardSetCommand = new BoardSetCommand(new Size(5, 5));
            _boardSetCommand.Set(_board);
            _boardSetCommand.Execute();

            _mineSetCommand = new MineSetCommand(new Location(locationX, locationY));
            _mineSetCommand.Set(_board, _mine);

            Assert.Throws<LocationValidationException>(() => _mineSetCommand.Execute());
        }

        [Fact]
        public void Turtle_Set_Command_Returns_Location_Tests()
        {
            _boardSetCommand = new BoardSetCommand(new Size(5, 4));
            _boardSetCommand.Set(_board);
            _boardSetCommand.Execute();

            _turtleSetCommand = new TurtleSetCommand(new TurtleLocation(1, 3, CardinalPoint.North));
            _turtleSetCommand.Set(_board, _turtle);
            _turtleSetCommand.Execute();

            Assert.Equal(1, _turtle.Location.X);
            Assert.Equal(3, _turtle.Location.Y);
            Assert.Equal(CardinalPoint.North, _turtle.Location.CardinalPoint);
        }

        [Theory]
        [InlineData(6, 1)]
        [InlineData(0, 6)]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        public void Turtle_Set_Command_Returns_Exception_Tests(int locationX, int locationY)
        {
            _boardSetCommand = new BoardSetCommand(new Size(5, 5));
            _boardSetCommand.Set(_board);
            _boardSetCommand.Execute();

            _turtleSetCommand = new TurtleSetCommand(new TurtleLocation(locationX, locationY, CardinalPoint.North));
            _turtleSetCommand.Set(_board, _turtle);

            Assert.Throws<LocationValidationException>(() => _turtleSetCommand.Execute());
        }

        [Fact]
        public void Turtle_Move_Command_Returns_Mine_Hit_And_Latest_Location_Tests()
        {
            var movements = new List<Move>
            {
                Move.Right,
                Move.Forward,
                Move.Left,
                Move.Forward
            };

            var mines = new List<IMine> {_mine};

            _boardSetCommand = new BoardSetCommand(new Size(5, 4));
            _boardSetCommand.Set(_board);
            _boardSetCommand.Execute();

            _mineSetCommand = new MineSetCommand(new Location(1, 2));
            _mineSetCommand.Set(_board, _mine);
            _mineSetCommand.Execute();

            _exitSetCommand = new ExitSetCommand(new Location(3, 3));
            _exitSetCommand.Set(_board, _exit);
            _exitSetCommand.Execute();

            _turtleSetCommand = new TurtleSetCommand(new TurtleLocation(0, 1, CardinalPoint.North));
            _turtleSetCommand.Set(_board, _turtle);
            _turtleSetCommand.Execute();

            _turtleMoveCommand = new TurtleMoveCommand(movements);
            _turtleMoveCommand.Set(_turtle, mines, _exit);
            _turtleMoveCommand.Execute();

            Assert.Equal("Mine Hit", _turtle.Result);

            Assert.Equal(1, _turtle.Location.X);
            Assert.Equal(2, _turtle.Location.Y);
            Assert.Equal(CardinalPoint.North, _turtle.Location.CardinalPoint);
        }

        [Fact]
        public void Turtle_Move_Command_Returns_Success_And_Latest_Location_Tests()
        {
            var movements = new List<Move>
            {
                Move.Right,
                Move.Forward,
                Move.Forward,
                Move.Forward
            };

            var mines = new List<IMine>();

            _boardSetCommand = new BoardSetCommand(new Size(5, 4));
            _boardSetCommand.Set(_board);
            _boardSetCommand.Execute();

            _exitSetCommand = new ExitSetCommand(new Location(3, 1));
            _exitSetCommand.Set(_board, _exit);
            _exitSetCommand.Execute();

            _turtleSetCommand = new TurtleSetCommand(new TurtleLocation(0, 1, CardinalPoint.North));
            _turtleSetCommand.Set(_board, _turtle);
            _turtleSetCommand.Execute();

            _turtleMoveCommand = new TurtleMoveCommand(movements);
            _turtleMoveCommand.Set(_turtle, mines, _exit);
            _turtleMoveCommand.Execute();

            Assert.Equal("Success", _turtle.Result);

            Assert.Equal(3, _turtle.Location.X);
            Assert.Equal(1, _turtle.Location.Y);
            Assert.Equal(CardinalPoint.East, _turtle.Location.CardinalPoint);
        }

        [Fact]
        public void Turtle_Move_Command_Returns_Still_In_Danger_And_Latest_Location_Tests()
        {
            var movements = new List<Move>
            {
                Move.Right,
                Move.Forward
            };

            var mines = new List<IMine>();

            _boardSetCommand = new BoardSetCommand(new Size(5, 4));
            _boardSetCommand.Set(_board);
            _boardSetCommand.Execute();

            _exitSetCommand = new ExitSetCommand(new Location(3, 3));
            _exitSetCommand.Set(_board, _exit);
            _exitSetCommand.Execute();

            _turtleSetCommand = new TurtleSetCommand(new TurtleLocation(0, 1, CardinalPoint.North));
            _turtleSetCommand.Set(_board, _turtle);
            _turtleSetCommand.Execute();

            _turtleMoveCommand = new TurtleMoveCommand(movements);
            _turtleMoveCommand.Set(_turtle, mines, _exit);
            _turtleMoveCommand.Execute();

            Assert.Equal("Still In Danger", _turtle.Result);

            Assert.Equal(1, _turtle.Location.X);
            Assert.Equal(1, _turtle.Location.Y);
            Assert.Equal(CardinalPoint.East, _turtle.Location.CardinalPoint);
        }

        [Fact]
        public void Turtle_Move_Command_Returns_Exception_Tests()
        {
            var movements = new List<Move>
            {
                Move.Right,
                Move.Forward,
                Move.Forward,
                Move.Forward,
                Move.Forward,
                Move.Forward,
                Move.Forward
            };

            var mines = new List<IMine>();

            _boardSetCommand = new BoardSetCommand(new Size(5, 4));
            _boardSetCommand.Set(_board);
            _boardSetCommand.Execute();

            _exitSetCommand = new ExitSetCommand(new Location(3, 1));
            _exitSetCommand.Set(_board, _exit);
            _exitSetCommand.Execute();

            _turtleSetCommand = new TurtleSetCommand(new TurtleLocation(0, 1, CardinalPoint.North));
            _turtleSetCommand.Set(_board, _turtle);
            _turtleSetCommand.Execute();

            _turtleMoveCommand = new TurtleMoveCommand(movements);
            _turtleMoveCommand.Set(_turtle, mines, _exit);
            _turtleMoveCommand.Execute();

            Assert.Throws<LocationValidationException>(() => _turtleMoveCommand.Execute());
        }
    }
}
