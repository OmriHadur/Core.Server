using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestApi.Common.Errors;
using RestApi.Standard.Shared.Errors;
using Unity;

namespace RestApi.Application.Application
{
    public class ApplicationBase
    {
        [Dependency]
        public IMapper Mapper { get; set; }

        //[Dependency]
        //public ILogger<ApplicationBase> Logger { get; set; }

        protected ActionResult BadRequest(BadRequestReason badRequestReason)
        {
            return new BadRequestApplicationResult(badRequestReason);
        }

        public ActionResult NotFound(string obj)
        {
            return new NotFoundApplicationResult(obj);
        }

        public ActionResult Ok()
        {
            return new OkResult();
        }

        public ActionResult Ok(object obj)
        {
            return new OkObjectResult(obj);
        }
    }
}
