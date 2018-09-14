using StudentWebService.Controllers.Interfaces;
using StudentWebService.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace StudentWebService.Controllers
{
    public abstract class BaseController : ApiController, IObjectController
    {
        // GET: api/Students
        [Route()]
        [HttpGet]
        public string Get()
        {
            return "Main page";
        }

        public string GetCurrentTime()
        {
            return DateTime.Now.ToString("T");
        }
    }
}