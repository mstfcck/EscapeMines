using EscapeMines.Abstractions;
using EscapeMines.Abstractions.Commands;
using EscapeMines.Abstractions.Services;
using EscapeMines.BoardObjects;
using EscapeMines.Commands;
using EscapeMines.Common;
using EscapeMines.Exceptions;
using EscapeMines.TurtleObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscapeMines.Services
{
    public class CommandParserService : ICommandParserService
    {
        private readonly IInstructionValidator _instructionValidator;

        public CommandParserService(IInstructionValidator instructionValidator)
        {
            _instructionValidator = instructionValidator;
        }

        public IEnumerable<ICommand> Parse(string instructions)
        {
            var instructionLines = instructions.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            _instructionValidator.IsValidInstructionsLength(instructionLines);

            var commands = new List<ICommand>();

            #region Line 1: The first line should define the board size

            var boardSize = instructionLines[0];
            if (_instructionValidator.IsValidBoardSize(boardSize))
            {
                commands.Add(ParseBoardSetCommand(boardSize));
            }

            #endregion

            #region Line 2: The second line should contain a list of mines (i.e. list of co-ordinates separate by a space)

            var listOfMines = instructionLines[1];
            if (_instructionValidator.IsValidListOfMines(listOfMines))
            {
                var location = listOfMines.Split(' '); // 1,1 1,3 3,3
                commands.AddRange(location.Select(ParseMineSetCommand));
            }

            #endregion

            #region Line 3: The third line of the file should contain the exit point

            var exitPoint = instructionLines[2];
            if (_instructionValidator.IsValidExitPoint(exitPoint))
            {
                commands.Add(ParseExitSetCommand(exitPoint));
            }

            #endregion

            #region Line 4: The fourth line of the file should contain the starting position of the turtle.

            var turtleStartingPosition = instructionLines[3];
            if (_instructionValidator.IsValidTurtleStartingPoint(turtleStartingPosition))
            {
                commands.Add(ParseTurtleSetCommand(turtleStartingPosition));
            }

            #endregion

            #region Line 5, ...: The fifth line to the end of the file should contain a series of moves.

            var movementInstructions = new StringBuilder();

            foreach (var instructionLine in instructionLines.Skip(4))
            {
                if (_instructionValidator.IsValidTurtleSeriesOfMoves(instructionLine))
                {
                    movementInstructions.AppendFormat("{0} ", instructionLine);
                }
            }

            if (!string.IsNullOrEmpty(movementInstructions.ToString()))
            {
                commands.Add(ParseTurtleMoveCommand(movementInstructions.ToString().Trim()));
            }

            #endregion

            return commands;
        }

        #region Private Helpers

        private ICommand ParseBoardSetCommand(string instruction)
        {
            // Sample instruction input: "5 4"
            var instructions = instruction.Split(' '); // 5, 4
            var x = int.Parse(instructions[0]); // 5
            var y = int.Parse(instructions[1]); // 4

            return new BoardSetCommand(new Size(x, y));
        }

        private ICommand ParseMineSetCommand(string instruction)
        {
            // Sample instruction input: "1,1"
            var instructions = instruction.Split(','); // 1, 2
            var x = int.Parse(instructions[0]); // 1
            var y = int.Parse(instructions[1]); // 2

            return new MineSetCommand(new Location(x, y));
        }

        private ICommand ParseExitSetCommand(string instruction)
        {
            // Sample instruction input: "1 2"
            var instructions = instruction.Split(' '); // 1, 2
            var x = int.Parse(instructions[0]); // 1
            var y = int.Parse(instructions[1]); // 2

            return new ExitSetCommand(new Location(x, y));
        }

        private ICommand ParseTurtleSetCommand(string instruction)
        {
            // Sample instruction input: "1 2 N"
            var instructions = instruction.Split(' '); // 1, 2, N
            var x = int.Parse(instructions[0]); // 1
            var y = int.Parse(instructions[1]); // 2
            var direction = instructions[2][0]; // N

            CardinalPoint cardinalPoint;

            switch (direction)
            {
                case 'N':
                    cardinalPoint = CardinalPoint.North;
                    break;
                case 'W':
                    cardinalPoint = CardinalPoint.West;
                    break;
                case 'S':
                    cardinalPoint = CardinalPoint.South;
                    break;
                case 'E':
                    cardinalPoint = CardinalPoint.East;
                    break;
                default:
                    throw new CommandValidationException($"'{instruction}' is not a valid instruction");
            }

            return new TurtleSetCommand(new TurtleLocation(x, y, cardinalPoint));
        }

        private ICommand ParseTurtleMoveCommand(string instruction)
        {
            // Sample instruction input: "R M L M M"
            var instructions = instruction.Split(' ');

            string ParseToEnumName(string value)
            {
                switch (value)
                {
                    case "M":
                        return "Forward";
                    case "R":
                        return "Right";
                    case "L":
                        return "Left";
                    default:
                        throw new CommandValidationException($"'{value}' is not a valid instruction");
                }
            }

            var moves = instructions.Select(move => Enum.Parse<Move>(ParseToEnumName(move))).ToList();

            return new TurtleMoveCommand(moves);
        }

        #endregion
    }
}