using Entities.LogModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Services.Contracts;

namespace Presentation.ActionFilters
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private readonly ILoggerService _loggerService;

        public LogFilterAttribute(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _loggerService.LogInfo(Log("OnActionExecuting", context.RouteData));
            base.OnActionExecuting(context);
        }

        private string Log(string modelName, RouteData routeData)
        {
            var logDetails = new LogDetails()
            {
                Action = routeData.Values["action"],
                Controller = routeData.Values["controller"],
                ModelName = modelName
            };

            return logDetails.ToString();
        }
    }
}
