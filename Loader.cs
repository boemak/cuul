using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace cuul
{
    
    class Loader
    {
        private const string _fileName = "cuul.conf";


        private string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _fileName);


        public List<Room> LoadConfig()
        {
            if (File.Exists(_filePath))
            {
                using(var reader = new StreamReader(new FileStream(_filePath, FileMode.Open)))
                {
                    Console.WriteLine("Reading config from: {0}", _filePath);
                    return JsonConvert.DeserializeObject<List<Room>>(reader.ReadToEnd());
                }
            }
            else
            {
                var start = new Room("The starting room", "Nothing special, it is a starting place allright");
                var hall = new Room("The main hall", "It is quite the hall, I wish I had one like this.");
                var library = new Room("The Library");
                var smokeroom = new Room("The smoke room");
                var classroom = new Room("The classroom", "It sure is a classroom");
                
                
                start.SetExit(Direction.North, hall);
                hall.SetExit(Direction.East, smokeroom);
                smokeroom.SetExit(Direction.West, hall);
                hall.SetExit(Direction.North, classroom);
                hall.SetExit(Direction.West, library);
                library.SetExit(Direction.East, hall);

                var rooms = new List<Room>()
                {
                    start,
                    hall,
                    library,
                    smokeroom,
                    classroom
                };


                using (var writer = new StreamWriter(new FileStream(_filePath, FileMode.CreateNew)))
                {
                    Console.WriteLine("Writing config at: {0}", _filePath);
                    writer.Write(JsonConvert.SerializeObject(rooms));
                    writer.Flush();
                }
                return rooms;
            }
        }
    }
}
