namespace SharpOMatic.DemoServer;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Test([FromForm] List<IFormFile>? images, IServiceProvider serviceProvider)
    {
        try
        {
            var engine = serviceProvider.GetRequiredService<IEngineService>();
            var workflowId = await engine.GetWorkflowId("Example Workflow");
            var runId = await engine.CreateWorkflowRun(workflowId);

            await engine.StartWorkflowRunAndNotify(runId);



            //var workflowId = await engine.GetWorkflowId("Test");
            //var runId = await engine.CreateWorkflowRun(workflowId);

            //ContextObject inputContext = [];
            //ContextList assetList = [];
            //inputContext.Add("image", assetList);

            //foreach (var image in images ?? [])
            //    if ((image is not null) && (image.Length > 0))
            //        assetList.Add(await image.CreateAssetRefAsync(assetService, AssetScope.Run, runId, image.Name, image.ContentType));

            //var completedRun = await engine.StartWorkflowRunAndWait(runId, inputContext);
            //ContextObject context = ContextObject.Deserialize(completedRun.OutputContext, serviceProvider);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
