using Systems.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        IEnumarable<Command> GetAppCommands();
        Command GetCommandById(int id);
    }
}