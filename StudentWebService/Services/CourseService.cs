using MongoDB.Bson;
using MongoDB.Driver;
using StudentWebService.Models;
using StudentWebService.Repositories;
using StudentWebService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentWebService.Services
{
    public class CourseService : IObjectService
    {
        private readonly BaseRepository<Course> _repoCourse =new CourseRepository();
        private readonly StudentService _studentService = new StudentService();

        public List<Course> GetObjectByFilter(FilterDefinition<Course> filter)
        {
            var objects = _repoCourse.GetFilteredCollection(filter).ToList();
            return objects.Count == 0 ? throw new Exception($"Brak Kursu o danych zmiennych: {filter.ToJson()}") : objects; 
        }

        public Course GetCourseByName(string name)
        {
            var objects = _repoCourse.GetObject(name);
            return objects ?? throw new Exception($"Brak kursu o nazwie: {name}");
        }

        public List<Student> GetAllStudentsForCourse(string id)
        {
            List<Student> allStudentsForCourse = new List<Student>();
            var course = GetCourseByName(id);
            var students = _studentService.GetAllObjects();
            bool exist;
            foreach (var student in students)
            {
                var exists = student.Courses.Find(x => x.Id == course.Id);
                if (exists != null) allStudentsForCourse.Add(student);
            }
            return allStudentsForCourse;
        }

        public bool UpdateCourse(Course course)
        {
            var updateDefinition = Builders<Course>.Update
                 .Set(x => x.LeadTeacher, course.LeadTeacher)
                 .Set(x => x.Points, course.Points);

            var result = _repoCourse.Update(course.Id, updateDefinition);
            return result.IsAcknowledged;
        }

        public List<Course> GetAllObjects()
        {
            var collection = _repoCourse.GetCollection().AsQueryable().ToList();
            return collection;
        }

        public bool DeleteCourse(string name)
        {
            var result = _repoCourse.Delete(name);
            return result.DeletedCount != 0;
        }

        public void AddCourse(Course value)
        {
            _repoCourse.AddObject(value);
        }
    }
}