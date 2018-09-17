﻿using MongoDB.Driver;
using StudentWebService.Controllers.Interfaces;
using StudentWebService.Models;
using StudentWebService.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentWebService.Controllers
{
    [RoutePrefix("courses")]
    public class CourseController : ApiController, IObjectController
    {
        private readonly CourseService _courseService = new CourseService();
       
        [HttpGet]
        public HttpResponseMessage GetCourses()
        {
            try
            {
                var list = _courseService.GetAllCourses().ToList();

                return Request.CreateResponse(list == null ? HttpStatusCode.OK : HttpStatusCode.NotFound, list);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [Route("{id}")]
        public HttpResponseMessage GetCourse(int id)
        {
            try
            {
                var student = _courseService.GetCourseByName(id.ToString());
                return Request.CreateResponse(student == null ? HttpStatusCode.OK : HttpStatusCode.NotFound, student);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        //Adding new// POST:
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Course value)
        {
            try
            {
                _courseService.AddCourse(value);
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
        public HttpResponseMessage Put(int id, [FromBody] Course value)
        {
            try
            {
                var result = _courseService.UpdateCourse(value);
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
                var result = _courseService.DeleteCourse(id.ToString());
                return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}