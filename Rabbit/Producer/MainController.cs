







using Microsoft.AspNetCore.Mvc;

[ApiController]
public class MainController : ControllerBase
{


    [HttpGet("index")]
    public void Index()
    {
        throw new Exception("asd");
    }
}