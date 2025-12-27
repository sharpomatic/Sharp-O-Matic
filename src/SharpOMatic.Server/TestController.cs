using Microsoft.AspNetCore.Mvc;
using SharpOMatic.Engine.Interfaces;

namespace SharpOMatic.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Test(IEngineService engine)
        {
            try
            {
                var workflowId = await engine.TryGetWorkflowId("Test");
                var run = await engine.RunWorkflowAndWait(workflowId);
                // var run = await engine.RunWorkflowAndNotify(workflowId);
                // var run = engine.RunWorkflowSynchronously(workflowId);
                return Ok(run.RunStatus.ToString());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
