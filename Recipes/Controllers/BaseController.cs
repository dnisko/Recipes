using Common.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Recipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult Response<TResult>(CustomResponse<TResult> response) where TResult : new()
        {
            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }
            return Ok(response.Result);
        }
        protected IActionResult Response(CustomResponse response)
        {
            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }
            return Ok();
        }
    }
}
