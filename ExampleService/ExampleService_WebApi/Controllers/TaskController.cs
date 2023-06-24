using Microsoft.AspNetCore.Mvc;

namespace ExampleService_WebApi;

[ApiController]
[Route("[controller]")]
public class TaskController :  ControllerBase
{   
    private readonly InterfaceClass _interface;

    //injecting the interface dependency
    public TaskController(InterfaceClass interfaceClass)
    {
        _interface= interfaceClass;
    }

    //to return the list of task
    [HttpGet("List of your Task")]
    public IActionResult Get()
    {   
        //db call
        var taskList = _interface.ListofTask();

        //mapping as DTO
        var taskMapping = new TaskDTO();
        foreach(var tasks in taskList)
        {
            taskMapping.Add(tasks);
        } 

        //returning as DTO
        return Ok(taskMapping);
    }

    //to add a new task 
    [HttpPost("Create your Task here")]
    public IActionResult Post([FromBody] TaskDTO task)
    {   
        //creating a inline mapping
        var taskMapping = new TaskModel();
        taskMapping.TaskStatus = task.TaskStatus;
            taskMapping.TaskName = task.TaskName;
                taskMapping.TaskDescription = task.TaskDescription;

        _interface.AddNewTask(taskMapping);
                
        return Ok("Task added successfully");
    }

}
