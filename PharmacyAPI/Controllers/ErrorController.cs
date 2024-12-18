using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PharmacyAPI.Controllers
{
    [ApiController]
    [Route("/error")]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult HandleError()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            // Returnează un mesaj standardizat
            return Problem(
                detail: exception?.Message,
                statusCode: 500,
                title: "An unexpected error occurred.");
        }
    }
}