﻿using MongoDB.Driver;
using StudentWebService.Controllers.Interfaces;
using StudentWebService.Models;
using StudentWebService.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;

namespace StudentWebService.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsController: ApiController, IObjectController
    {
       
        private readonly StudentService _studentService = new StudentService();

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetObjectByParameters(string id = null, string name = null, string surname = null, string method = "after", string birthdate = null)
        {
            try
            {
                var builder = Builders<Student>.Filter;
                FilterDefinition<Student> filter = null;
                if (!string.IsNullOrEmpty(id))
                {
                    filter = builder.Eq(item => item.Id, id);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    filter = filter == null ? builder.Eq(item => item.Name, name) : filter & builder.Eq(item => item.Name, name);
                }
                if (!string.IsNullOrEmpty(surname ))
                {
                    filter = filter == null ? builder.Eq(item => item.Surname, surname) : filter & builder.Eq(item => item.Surname, surname);
                }
                if (!string.IsNullOrEmpty(birthdate))
                {
                    var date = DateTime.Parse(birthdate);
                    if (method == "after")
                    {
                        filter = filter == null ? builder.Where(item => item.BirthDate > date) : filter & builder.Where(item => item.BirthDate > date);
                    }
                    else
                    {
                        filter = filter == null ? builder.Where(item => item.BirthDate < date) : filter & builder.Where(item => item.BirthDate < date);
                    }
                }

                var students = filter == null ? _studentService.GetAllObjects() : _studentService.GetObjectByFilter(filter);

                if (students != null)
                {
                    return Ok(students);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [Route("{id}/courses")]
        [HttpGet]
        public IHttpActionResult GetAllStudentCourses(int id)
        {
            try
            {
                var courses = _studentService.GetStudentCourses(id.ToString());

                if (courses.Count != 0)
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

        [Route("{id}", Name = "GetStudentByIndex")]
        [HttpGet]
        public IHttpActionResult GetObject(string id)
        {
            try
            {
                var student = _studentService.GetStudentByIndex(id);

                if (student != null)
                {
                    return Ok(student);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [Route("")]
        //Adding new// POST: api/Students
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Student value)
        {
            try
            {
                _studentService.AddObject(value);
                var response  = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Url.Link("GetStudentByIndex", new{id = value.Id}));
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [Route("{id}/courses/{name}")]
        //Adding new// POST: api/Students
        [HttpPost]
        public HttpResponseMessage Post(string id, string name)
        {
            try
            {
                _studentService.AddCourseToStudentByName(id,name);
                var response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Url.Link("GetStudentByIndex", new {id }));
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
        public IHttpActionResult Put(string id, [FromBody] Student value)
        {
            try
            {
                _studentService.GetStudentByIndex(id);
                var result = _studentService.UpdateStudent(value);
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
        // DELETE: api/Students/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _studentService.DeleteStudent(id.ToString());
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