namespace SharpOMatic.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToolController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> GetTypeSchemaNames(IToolMethodRegistry toolMethodRegistry)
    {
        return toolMethodRegistry.GetMethodDisplayNames();
    }
}
