using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        {
            //call constructor
        }
        //create repesentation of command model in database using db set (entity framework)
        public DbSet<Command> Commands { get; set; }
    }
}