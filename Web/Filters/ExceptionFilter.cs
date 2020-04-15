using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Web.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var argumentException = context.Exception is ArgumentException;
            var argumentOutOfRangeException = context.Exception is ArgumentOutOfRangeException;
            var invalidOperationException = context.Exception is InvalidOperationException;

            if (argumentException || argumentOutOfRangeException || invalidOperationException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message }
                //stackTrace = context.Exception.StackTrace
            });
        }
    }
}
