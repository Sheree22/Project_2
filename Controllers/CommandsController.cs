
using System.Collections.Generic;
using Commander.Models;
using Commander.Data; 
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    //api/commands
    [Route("api/[commands]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository; //dependency injection

        public CommandsController(ICommanderRepo repository)
        {
            _repository = repository; //whatever is injected goes to repository
        }
        //private readonly MockCommanderRepo _repository = new MockCommanderRepo();
        //GET respond
        //api/commands
        [HttpGet]
        public ActionResult <IEnumerable<Command>> GetAllCommands()
        {
            //variable to hold results
            var commandItems = _repository.GetAppCommands();
            return Ok(commandItems);
        }
        //GET request
        //api/commands/({id})
        [HttpGet("{id}")]
        public ActionResult <Command> GetCommandById(int id) //id comes from request
        {
            //variable to contain singel command item
            var commandItem = _repository.GetCommandById(id);
            return Ok(commandItems);
        }
    }
}