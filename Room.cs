using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cuul
{
    public class Room
    {
        public string Description { get; set; }
        public string LongDescription { get; set; }
        private Dictionary<Direction, Room> _exits;
        public Room(string description)
        {
            Description = description;
            _exits = new Dictionary<Direction, Room>();
            
            
        }
        //Constructor takes two arguments
        public Room(string description, string longDescription)
        {
            Description = description;
            LongDescription = longDescription;
            _exits = new Dictionary<Direction, Room>();
        }


        //Set room exits
        public void SetExit(Direction direction, Room room)
        {
            _exits.Add(direction, room);
        }

        public bool ContainsExit(Direction direction)
        {
            return _exits.ContainsKey(direction);
        }

        public Room MoveToRoom(Direction direction)
        {
            return _exits[direction];
        }
    }
}
