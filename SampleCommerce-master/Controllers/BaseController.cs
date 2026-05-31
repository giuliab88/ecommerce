using Microsoft.AspNetCore.Mvc;
using SampleCommerce.Common;

namespace SampleCommerce.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult ProcessResult(Result result, Func<IActionResult> onSuccess)
        {
            if (!result.Success)
            {
                if (string.Equals(result.Message, ErrorMessages.UserNotFound))
                    return NotFound(result);
                return BadRequest(result);
            }
            return onSuccess();
        }
    }
}