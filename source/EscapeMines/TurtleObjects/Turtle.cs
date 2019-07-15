using EscapeMines.Abstractions;
using EscapeMines.Abstractions.Services;
using EscapeMines.Exceptions;
using System.Collections.Generic;

namespace EscapeMines.TurtleObjects
{
    public class Turtle : ITurtle
    {
        private readonly ITurtleMoveService _turtleMoveService;

        public Turtle(ITurtleMoveService turtleMoveService)
        {
            _turtleMoveService = turtleMoveService;
        }

        private IBoard Board { get; set; }

        public TurtleLocation Location { get; set; }
        public string Result { get; set; }

        public void Set(IBoard board, TurtleLocation location)
        {
            if (board.IsValid(location))
                Location = location;
            else
                throw new LocationValidationException("Turtle location is not valid!");

            Board = board;
        }

        public void Move(IEnumerable<Move> movements, IEnumerable<IMine> mines, IExit exit)
        {
            var isHitMine = false;
            var isExit = false;

            foreach (var movement in movements)
            {
                if (movement == TurtleObjects.Move.Forward)
                {
                    // Make sure the turtle still in the board.
                    if (!Board.IsValid(Location))
                        throw new LocationValidationException("Turtle cannot go out of board!");

                    // Move the turtle to the next tile.
                    _turtleMoveService.MoveForward(Location);

                    // If the turtle hits the mine it will not continue.
                    if (_turtleMoveService.IsMineHit(Location, mines))
                    {
                        Result = $"Mine Hit";
                        isHitMine = true;
                        break;
                    }

                    // If the turtle finds the exit it will not continue.
                    if (_turtleMoveService.IsExit(Location, exit))
                    {
                        Result = $"Success";
                        isExit = true;
                        break;
                    }
                }
                else if (movement == TurtleObjects.Move.Left)
                {
                    // Turn left the turtle.
                    _turtleMoveService.MoveLeft(Location);
                }
                else if (movement == TurtleObjects.Move.Right)
                {
                    // Turn right the turtle.
                    _turtleMoveService.MoveRight(Location);
                }
            }

            if (!isHitMine && !isExit) Result = "Still In Danger";
        }
    }
}