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
    [RoutePrefix("students")]
    public class StudentsController: ApiController, IObjectController
    {
       
        private readonly StudentService _studentService = new StudentService();

        //GET: api/Students
        [HttpGet]
        public HttpResponseMessage GetAllObjects()
        {
            try
            {
                var list = _studentService.GetAllStudents().ToList();

                return Request.CreateResponse(list == null ? HttpStatusCode.OK : HttpStatusCode.NotFound,list);
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
                var student = _studentService.GetStudentByIndex(id.ToString());
                return Request.CreateResponse(student == null ? HttpStatusCode.OK : HttpStatusCode.NotFound,student);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        //Adding new// POST: api/Students
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Student value)
        {
            try
            {
                _studentService.AddObject(value);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }

        }

        // PUT: api/Students/5
        [Route("{id}")]
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] Student value)
        {
            try
            {
                var result = _studentService.UpdateStudent(value);
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
                var result = _studentService.DeleteStudent(id.ToString());
                return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
        
    }
}