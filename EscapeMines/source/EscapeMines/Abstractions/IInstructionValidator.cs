namespace EscapeMines.Abstractions
{
    public interface IInstructionValidator
    {
        void IsValidInstructionsLength(string[] instructions);
        bool IsValidBoardSize(string boardSize);
        bool IsValidListOfMines(string listOfMines);
        bool IsValidExitPoint(string exitPoint);
        bool IsValidTurtleStartingPoint(string turtleStartingPoint);
        bool IsValidTurtleSeriesOfMoves(string turtleStartingPoint);
    }
}