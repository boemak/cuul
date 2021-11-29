using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cuul
{
    public class Parser
    {
        private CommandWords _commandWords;

        public Parser()
        {
            _commandWords = new CommandWords();
        }
        public Tuple<bool,Command> Parse(string input)
        {
            var sanitized = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            return _commandWords.CreateCommand(sanitized);       
        }

        public List<string> GetCommands()
        {
            return _commandWords.GetCommands();
        }
    }
}
