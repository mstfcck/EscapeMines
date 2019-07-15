using EscapeMines.Abstractions;
using EscapeMines.Exceptions;
using System.Text.RegularExpressions;

namespace EscapeMines.Validators
{
    public class InstructionValidator : IInstructionValidator
    {
        public void IsValidInstructionsLength(string[] instructions)
        {
            if (instructions.Length < 5)
            {
                throw new InstructionValidationException("Instructions are missing. Please check again!");
            }
        }
        public bool IsValidBoardSize(string boardSize)
        {
            return new Regex(@"^\d+ \d+$").IsMatch(boardSize);
        }

        public bool IsValidListOfMines(string listOfMines)
        {
            return new Regex(@"").IsMatch(listOfMines);
        }

        public bool IsValidExitPoint(string exitPoint)
        {
            return new Regex(@"^\d+ \d+$").IsMatch(exitPoint);
        }

        public bool IsValidTurtleStartingPoint(string turtleStartingPoint)
        {
            return new Regex(@"^\d+ \d+ [NSEW]$").IsMatch(turtleStartingPoint);
        }

        public bool IsValidTurtleSeriesOfMoves(string turtleStartingPoint)
        {
            return new Regex(@"^[L R M]+$").IsMatch(turtleStartingPoint);
        }
    }
}
