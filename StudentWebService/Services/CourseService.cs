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
        private BaseRepository<Course> _repoCourse;
        public BaseRepository<Course> CourseRepository => _repoCourse ?? (_repoCourse = new CourseRepository());

        private StudentService _courseService;
        public StudentService StudentService => _courseService ?? (_courseService = new StudentService());

        public List<Course> GetObjectByFilter(FilterDefinition<Course> filter)
        {
            var objects = CourseRepository.GetFilteredCollection(filter).ToList();
            return objects;
        }

        public Course GetCourseByName(string name)
        {
            var objects = CourseRepository.GetObject(name);
            return objects;
        }

        public List<Student> GetAllStudentsForCourse(string id)
        {
            List<Student> allStudentsForCourse = new List<Student>();
            var course = GetCourseByName(id);
            var students = StudentService.GetAllObjects();
            
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

            var result = CourseRepository.Update(course.Id, updateDefinition);
            return result.IsAcknowledged;
        }

        public List<Course> GetAllObjects()
        {
            var collection = CourseRepository.GetCollection().AsQueryable().ToList();
            return collection;
        }

        public bool DeleteCourse(string name)
        {
            var result = CourseRepository.Delete(name);
            return result.DeletedCount != 0;
        }

        public void AddCourse(Course value)
        {
            CourseRepository.AddObject(value);
        }

        public CourseService()
        {

        }

        public CourseService(StudentService studentService)
        {
            _courseService = studentService;
        }
    }
}