using MongoDB.Driver;
using StudentWebService.Controllers.Interfaces;
using StudentWebService.Models;
using StudentWebService.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentWebService.Controllers
{
    [RoutePrefix("marks")]
    public class MarksController : ApiController, IObjectController
    {
        private readonly MarkService _markService = new MarkService();
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetObjectByParameters(string courseName = null, string studentIndex = null, string evaluation = null, string method = "more")
        {
            try
            {
                var builder = Builders<Mark>.Filter;
                FilterDefinition<Mark> filter = null;
                if (courseName != null)
                {
                    filter = builder.Eq(item => item.CourseId, courseName);
                }
                if (studentIndex != null)
                {
                    filter = filter == null ? builder.Eq(item => item.StudentId, studentIndex) : filter & builder.Eq(item => item.StudentId, studentIndex);
                }
                if (evaluation != null)
                {
                    if (method == "more")
                    {
                        filter = filter == null ? builder.Where(item => item.Evaliation > Convert.ToDecimal(evaluation)) : filter & builder.Where(item => item.Evaliation > Convert.ToDecimal(evaluation));
                    }
                    else
                    {
                        filter = filter == null ? builder.Where(item => item.Evaliation < Convert.ToDecimal(evaluation)) : filter & builder.Where(item => item.Evaliation < Convert.ToDecimal(evaluation));
                    }
                }

                var courses = filter == null ? _markService.GetAllObjects() : _markService.GetObjectByFilter(filter);

                if (courses != null)
                {
                    return Ok(courses);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [Route("{id}", Name = "GetMarkById")]
        [HttpGet]
        public IHttpActionResult GetObject(string id)
        {
            try
            {
                var course = _markService.GetObjectById(id);

                if (course != null)
                {
                    return Ok(course);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Mark value)
        {
            try
            {
                _markService.AddObject(value);
                var response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Url.Link("GetMarkById", new { id = value.Id }));
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(string id, [FromBody] Mark value)
        {
            try
            {
                _markService.GetObjectById(id);
                var result = _markService.UpdateObject(value);
                if (result)
                {
                    return Ok();
                }
                return Content(HttpStatusCode.NotModified, "Nie zmodyfikowano");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _markService.DeleteObject(id.ToString());
                if (result)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}