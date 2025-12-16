namespace SharpOMatic.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TypeSchemaController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> GetTypeSchemaNames(ITypeSchemaService typeSchemaService)
    {
        return typeSchemaService.GetTypeNames();
    }

    [HttpGet("{typeName}")]
    public string GetTypeSchemaNames(ITypeSchemaService typeSchemaService, string typeName)
    {
        return typeSchemaService.GetSchema(typeName);
    }
}
