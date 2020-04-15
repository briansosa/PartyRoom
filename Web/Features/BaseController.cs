using Common.Functional;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Features
{
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult Result(Result result)
        {
            if (result.IsSuccess) return Ok();
            else return BadRequest(result.ErrorMessage);
        }
        protected ActionResult Result<T>(Result<T> result) 
        {
            if (result.IsSuccess) return Ok(result.Value);
            else return BadRequest(result.ErrorMessage);
        }
    }
}
