using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestApi.Common.Errors;
using RestApi.Shared.Errors;
using System;
using Unity;

namespace RestApi.Application
{
    public class ApplicationBase
    {
        [Dependency]
        public IMapper Mapper { get; set; }

        //[Dependency]
        //public ILogger<ApplicationBase> Logger { get; set; }

        protected ActionResult BadRequest(BadRequestReason badRequestReason)
        {
            return BadRequest<BadRequestReason>(badRequestReason);
        }

        protected ActionResult BadRequest<TReson>(TReson badRequestReason)
            where TReson : struct, Enum
        {
            return new BadRequestApplicationResult(
                Convert.ToInt32(badRequestReason), 
                Enum.GetName(typeof(TReson), badRequestReason));
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
