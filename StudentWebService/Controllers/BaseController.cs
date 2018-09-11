using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Http;
using StudentWebService.Controllers.Interfaces;
using StudentWebService.Models;

namespace StudentWebService.Controllers
{
    public abstract class BaseController : ApiController, IObjectController
    {
        // GET: api/Students
        [Route()]
        [HttpGet]
        public IEnumerable<T> Get<T>()
        {
            return new List<T>();
        }

        // GET: api/Students/5
        [Route("{id}")]
        [HttpGet]
        public Object Get<T>(int id)
        {
            return new object();
        }

        //Adding new// POST: api/Students
        [HttpPost]
        public void Post([FromBody]Student value)
        {
            //studentsList.Add(value);
        }

        // PUT: api/Students/5
        [Route("{id}")]
        [HttpPut]
        public void Put<T>(int id, [FromBody]Object value)
        {
            Student update = new Student();
        }

        // DELETE: api/Students/5
        [Route("{id}")]
        [HttpDelete]
        public void Delete(int id)
        {

        }
    }
}