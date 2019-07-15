using EscapeMines.Abstractions;
using EscapeMines.Common;
using EscapeMines.Exceptions;

namespace EscapeMines.BoardObjects
{
    public class Mine : IMine
    {
        public Location Location { get; set; }

        public void Set(IBoard board, Location location)
        {
            if (board.IsValid(location))
            {
                Location = location;
            }
            else
            {
                throw new LocationValidationException("Mine location is not valid!");
            }
        }
    }
}
