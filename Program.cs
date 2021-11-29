using System;
using System.Collections.Generic;

namespace cuul
{
    //Direction enum
    public enum Direction
    {
        North,
        East,
        South,
        West
    }



    class Program
    {
        //Visited rooms stack. Makes it possible to go back
        public Stack<Room> VisitedRooms = new Stack<Room>();
        //Our current room
        public Room CurrentRoom;
        //The command parser
        public Parser parser;

       


        static void Main(string[] args)
        {
            //Instantiate a new Program as program and call the Run method on it.
            Program program = new Program();
            program.Run();
        }
        //Our Run method.
        public void Run()
        {
            Init();
        }
        //Initialise some things.
        public void Init()
        {
            //Create a parser to actually parse our commands.
            parser = new Parser();
            //Build the rooms layout
            CreateRooms();
            //Run the Play Method to start the main loop.
            Play();


        }
        //Create our rooms for the player to play in.
        private void CreateRooms()
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
            library.SetExit(Direction.East,hall);
            //Push our starting room on the stack.
            VisitedRooms.Push(start);
            //Set the starting room as the CurrentRoom
            SetCurrentRoom(start);

        }

        private void SetCurrentRoom(Room room)
        {
            Console.WriteLine("You are in: {0}", room.Description);           
            CurrentRoom = room;
        }
        

        private void Play()
        {
            //Welcome text for the player
            Console.WriteLine("Welcome to Cuul");
            Console.WriteLine("type help for a list of commands");
            //Our main loop
            while(true)
            {
                Console.WriteLine("Input");
                //Read input from the keyboard
                var input = Console.ReadLine();
                //Parse the input into a valid command if possible.
                var command = parser.Parse(input);
                if(command.Item1)
                {
                    Console.WriteLine("valid input");
                    Console.WriteLine("You wrote: {0} {1}", command.Item2.FirstWord, command.Item2.SecondWord);
                    ExcuteCommand(command.Item2);
                }
                else
                {
                    Console.WriteLine("I don't understand....");                }
                
            }
        }

        private void ExcuteCommand(Command command)
        {
            switch (command.FirstWord)
            {
                case "go":
                    GoRoom(command);
                    break;
                case "quit":
                    Console.WriteLine("Goodbye! Any key to quit");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                case "inspect":
                    Inspect(CurrentRoom);
                    break;
                case "back":
                    GoBack();
                    break;
                case "help":
                    PrintHelp();
                    break;

                default:
                    break;
            }
        }

        private void PrintHelp()
        {
            Console.WriteLine("Valid commands are:");
            var commands = parser.GetCommands();
            foreach(var command in commands)
            {
                Console.Write(" {0} ", command);
            }
            Console.WriteLine();
        }

        private void Inspect(Room room)
        {
            if (!String.IsNullOrEmpty(room.LongDescription))
            {
                Console.WriteLine(room.LongDescription);
            }
            else
            {
                Console.WriteLine("Even on closer inspection there is nothing remarkable about this");
            }
        }

        private void GoBack()
        {
            if (VisitedRooms.Count > 1)
            {
                SetCurrentRoom(VisitedRooms.Pop());
            }
            else
            {
                Console.WriteLine("We are already at the start!");
            }
        }
        //Move to a new room
        private void GoRoom(Command command)
        {
            //Do we even have a second word in our command? If it is empty we don't know where to move.
            if(String.IsNullOrEmpty(command.SecondWord))
            {
                Console.WriteLine("Go where?");
            }
            //we seem to have a second word in our command
            else
            {
                //Direction variable
                Direction dir;
                //Try to create an enum from the typed in direction
                if(Enum.TryParse<Direction>(command.SecondWord,true, out dir))
                {
                    //Does the current room contain an exit in this direction?
                    if(CurrentRoom.ContainsExit(dir))
                    {
                        //Seems so
                        //We push the currentroom on the stack.
                        VisitedRooms.Push(CurrentRoom);
                        //We set our new room as the current room.
                        SetCurrentRoom(CurrentRoom.MoveToRoom(dir));
                    }
                    else
                    {
                        Console.WriteLine("No exit here!");
                    }
                }
                //Seems like we can't parse the input. 
                else
                {
                    //Show what the user has typed.
                    Console.WriteLine("I don't know that direction: {0}", command.SecondWord);
                }

            }

        }
    }
}
