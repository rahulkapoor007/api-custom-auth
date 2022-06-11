using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthorizationAndAuthentication.Controllers
{
    public abstract class BaseController : Controller
    {
        public HttpRequest httpRequest { get; private set; }
        public int UserId { get; private set; }
        public int LanguageId { get; private set; }
        protected string AuthorizationToken =>
            httpRequest.Headers["Authorization"].FirstOrDefault();

        protected string RemoteIpAddress =>
             httpRequest.Headers["True-Client-IP"].FirstOrDefault() ?? httpRequest?.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();

        protected string ApiKey =>
            httpRequest.Headers["api_key"].FirstOrDefault();

        protected string Offset =>
            httpRequest.Headers["offset"].FirstOrDefault();

        protected string Language =>
        httpRequest.Headers["language"].FirstOrDefault();

        protected string ContentType =>
        httpRequest.Headers["Content-Type"].FirstOrDefault();

        protected string Timezone =>
        httpRequest.Headers["timezone"].FirstOrDefault();
        public BaseController()
        {
            this.httpRequest = Request;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.httpRequest = Request;
            var Id = (HttpContext.Items.ContainsKey("UserId") ? HttpContext.Items["UserId"] : null);
            if (Id == null) this.UserId = -1;
            else this.UserId = (int)Id;
            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
