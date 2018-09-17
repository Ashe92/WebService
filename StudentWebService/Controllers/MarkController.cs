using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Driver;
using StudentWebService.Controllers.Interfaces;
using StudentWebService.Models;
using StudentWebService.Services;

namespace StudentWebService.Controllers
{
    [RoutePrefix("marks")]
    public class MarkController : ApiController, IObjectController
    {
        private readonly MarkService _markService = new MarkService();

        [HttpGet]
        public HttpResponseMessage GetAllObjects()
        {
            try
            {
                var list = _markService.GetAllObjects().ToList();

                return Request.CreateResponse(list == null ? HttpStatusCode.OK : HttpStatusCode.NotFound, list);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [Route("{id}")]
        public HttpResponseMessage GetObject(int id)
        {
            try
            {
                var student = _markService.GetObjectByName(id.ToString());
                return Request.CreateResponse(student == null ? HttpStatusCode.OK : HttpStatusCode.NotFound, student);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        //Adding new// POST:
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Mark value)
        {
            try
            {
                _markService.AddObject(value);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }

        }

        // PUT:
        [Route("{id}")]
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] Mark value)
        {
            try
            {
                var result = _markService.UpdateObject(value);
                return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotModified);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        // DELETE: api/Students/5
        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = _markService.DeleteObject(id.ToString());
                return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}