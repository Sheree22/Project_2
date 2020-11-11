using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAppCommands()
        {
            var commands = new List<Command>
            {
                new Command(Id=0, Age=20),
                new Command(Id=1, Age=21),
                new Command(Id=2, Age=22)
            };

            return commands;
        
        public Command GetCommandById(int id)
        {
            return new Command(Id=0, Age=20);
        }
    }
}