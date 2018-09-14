using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using StudentWebService.Controllers.Interfaces;
using System.Web.Http;
using System.Windows.Forms;
using StudentWebService.Models;
using StudentWebService.Services;
using Exception = System.Exception;

namespace StudentWebService.Controllers
{
    public class StudentController: ApiController, IObjectController
    {
        [RoutePrefix("students")]
        public class StudentsController : ApiController
        {
            private readonly StudentService _studentService =new StudentService();
            
            //GET: api/Students
            [HttpGet]
            public HttpResponseMessage Get()
            {
                try
                {
                    var list = _studentService.GetAllStudents();
                    return Request.CreateResponse(list == null ? HttpStatusCode.OK : HttpStatusCode.NotFound);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($@"Błąd {ex.Message}");
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                }
            }

            [HttpGet]
            public HttpResponseMessage Get(string index)
            {
                try
                {
                    var student = _studentService.GetStudentByIndex(index);
                    return Request.CreateResponse(student==null ? HttpStatusCode.OK : HttpStatusCode.NotFound);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($@"Błąd {ex.Message}");
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
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
                    MessageBox.Show($@"Błąd {ex.Message}");
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                }
               
            }

            // PUT: api/Students/5
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
                    MessageBox.Show($@"Błąd {ex.Message}");
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                }
            }

            // DELETE: api/Students/5
            [HttpDelete]
            public HttpResponseMessage Delete(string index)
            {
                try
                {
                    var result = _studentService.DeleteStudent(index);
                    return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($@"Błąd {ex.Message}");
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                }
            }
        }
    }
}