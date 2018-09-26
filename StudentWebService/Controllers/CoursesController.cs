using MongoDB.Driver;
using StudentWebService.Controllers.Interfaces;
using StudentWebService.Models;
using StudentWebService.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentWebService.Controllers
{
    [RoutePrefix("courses")]
    public class CoursesController : ApiController, IObjectController
    {
        private readonly CourseService _courseService = new CourseService();

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetObjectByParameters(string id = null, string leadTeacher = null, string points = null, string method ="more")
        {
            try
            {
                var builder = Builders<Course>.Filter;
                FilterDefinition<Course> filter = null;
                if (!string.IsNullOrEmpty(id))
                {
                    filter = builder.Eq(item => item.CourseName, id);
                }
                if (!string.IsNullOrEmpty(leadTeacher))
                {
                    filter = filter == null ? builder.Eq(item => item.LeadTeacher, leadTeacher) : filter & builder.Eq(item => item.LeadTeacher, leadTeacher);
                }
                if (!string.IsNullOrEmpty(points))
                {
                    if (method == "more")
                    {
                        filter = filter == null ? builder.Where(item => Convert.ToInt32(item.Points) > Convert.ToInt32(points)) : filter & builder.Where(item => Convert.ToInt32(item.Points) > Convert.ToInt32(points));
                    }
                    else
                    {
                        filter = filter == null ? builder.Where(item => Convert.ToInt32(item.Points) < Convert.ToInt32(points)) : filter & builder.Where(item => Convert.ToInt32(item.Points) < Convert.ToInt32(points));
                    }
                }

                var courses = filter == null ? _courseService.GetAllObjects() : _courseService.GetObjectByFilter(filter);

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

        [Route("{id}", Name = "GetCourseByName")]
        [HttpGet]
        public IHttpActionResult GetObject(string id)
        {
            try
            {
                var course = _courseService.GetCourseByName(id);

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

        [Route("{id}/students")]
        [HttpGet]
        public IHttpActionResult GetStudentsForCourses(string id)
        {
            try
            {
                var allStudentsForCourse = _courseService.GetAllStudentsForCourse(id);
                if (allStudentsForCourse.Count !=0)
                {
                    return Ok(allStudentsForCourse);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [Route("")]
        //Adding new// POST: api/Course
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Course value)
        {
            try
            {
                _courseService.AddCourse(value);
                var response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Url.Link("GetCourseByIndex", new { id = value.Id }));
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
        
        [Route("{id}")]
        // PUT: api/Students/5
        [HttpPut]
        public IHttpActionResult Put(string id, [FromBody] Course value)
        {
            try
            {
                _courseService.GetCourseByName(id);
                var result = _courseService.UpdateCourse(value);
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
        // DELETE: api/Courses/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _courseService.DeleteCourse(id.ToString());
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