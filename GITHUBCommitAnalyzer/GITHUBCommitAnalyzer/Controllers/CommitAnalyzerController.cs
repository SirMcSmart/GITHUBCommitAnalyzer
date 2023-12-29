using GITHUBCommitAnalyzer.BaseUtilities;
using GITHUBCommitAnalyzer.Interfaces;
using GITHUBCommitAnalyzer.Services;
using GITHUBCommitAnalyzer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GITHUBCommitAnalyzer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommitAnalyzerController : ControllerBase
    {
        private readonly ICommitAnalyzerService _commitAnalyzerService;

        public CommitAnalyzerController(ICommitAnalyzerService commitAnalyzerService)
        {
            _commitAnalyzerService = commitAnalyzerService;
        }

        [Route("GITHUBAnalyzer")]
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseVM<GITHUBAnalyzerResponse>), 200)]
        public async Task<IActionResult> GITHUBAnalyzer([FromBody] GITHUBAnalyzerRequest model)
        {
            if (model == null||model.Username == "string" || model.RepoName == "string" || !ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return BadRequest(ResponseMessage.Response(ResponseCode.Error, ResponseDescription.Error, "ModelState not valid or request is null! Please check the payload details and try again..  " + messages));
            }

            var response = await _commitAnalyzerService.GITHUBAnalyzer(model);
            if (response.StatusCode == 200) return Ok(ResponseMessage.Response(ResponseCode.Created, ResponseDescription.Success, response, true));
            if (response.StatusCode == 412) return NotFound(ResponseMessage.Response(ResponseCode.Error, ResponseDescription.Error, response.Description));
            if (response.StatusCode == 422) return UnprocessableEntity(ResponseMessage.Response(ResponseCode.Error, ResponseDescription.Error, response.Description));
            if (response.StatusCode == 400) return BadRequest(ResponseMessage.Response(ResponseCode.Error, ResponseDescription.Error, response.Description));
            return BadRequest(ResponseMessage.Response(ResponseCode.Error, ResponseDescription.Error, "Unable to process request. The server cannot meet the requirements"));
        }
    }
}
