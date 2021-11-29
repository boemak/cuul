using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cuul
{
    public class Command
    {
        public string FirstWord { get; set; }
        public string SecondWord { get; set; }
    }
    public class CommandWords
    {
        private List<string> _commands;
        public CommandWords()
        {
            _commands = new List<string>()
            {
                "go",
                "inspect",
                "quit",
                "back",
                "help"
            };

        }

        public Tuple<bool,Command> CreateCommand(string[] words)
        {
            var first = words[0];
            string secondWord = "";
            if(words.Length > 1)
            {
                secondWord = words[1];
            }

            if(_commands.Contains(first))
            {
                return new Tuple<bool, Command>(true, new Command() { FirstWord = first, SecondWord = secondWord});
            }
            else
            {
                return new Tuple<bool, Command>(false, null);
            }
        }

        public List<string> GetCommands()
        {
            return _commands;
        }

        
    }
}
