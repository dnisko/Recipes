using Common.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Recipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult Response<TResult>(CustomResponse<TResult> response) where TResult : class, new()
        {
            if (response == null)
            {
                return Problem(
                    detail: "Unexpected null response from service.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }

            if (!response.IsSuccessful)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = response.Errors
                });
            }

            return Ok(new
            {
                success = true,
                message = response.SuccessMessage,
                data = response.Result
            });
        }

        protected IActionResult Response(CustomResponse response)
        {
            if (response == null)
            {
                return Problem(
                    detail: "Unexpected null response from service.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }

            if (!response.IsSuccessful)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = response.Errors
                });
            }

            return Ok(new
            {
                success = true,
                message = response.SuccessMessage
            });
        }
    }
}