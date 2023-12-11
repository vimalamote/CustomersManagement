using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementMvc.Controllers
{
    public class ErrorController : Controller
    {
        ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            this._logger = logger;
        }
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult =
           HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested cannot be found";
                    _logger.LogWarning($"404 error occured. Path = " +
                    $"{statusCodeResult.OriginalPath} and QueryString = " +
                    $"{statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");


        }
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"The path {exceptionHandlerPathFeature.Path}" +
                $"threw an exception {exceptionHandlerPathFeature.Error}");

            return View("Error");
        }
    }
}
