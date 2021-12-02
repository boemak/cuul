using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cuul
{
    
    public class Room
    {
        public string Description { get; set; }
        public string LongDescription { get; set; }

        public List<Item> Items = new List<Item>();

        [JsonProperty]
        public Dictionary<Direction, Guid> _exits;
        public Guid Id;

        [JsonConstructor]
        public Room(string description)
        {
            Description = description;
            Id = Guid.NewGuid();
            _exits = new Dictionary<Direction, Guid>();




        }
        //Constructor takes two arguments
        public Room(string description, string longDescription) 
        : this(description)
        {
            
            LongDescription = longDescription;

        }


        //Set room exits
        public void SetExit(Direction direction, Room room)
        {
            _exits.Add(direction, room.Id);
        }

        public bool ContainsExit(Direction direction)
        {
            return _exits.ContainsKey(direction);
        }

        public Guid GetExit(Direction direction)
        {
            return _exits[direction];
        }
    }
}
