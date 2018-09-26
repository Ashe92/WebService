using MongoDB.Bson;
using MongoDB.Driver;
using StudentWebService.Models;
using StudentWebService.Repositories;
using StudentWebService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace StudentWebService.Services
{
    public class StudentService : IObjectService
    {
        private BaseRepository<Student> _repoStudent ;
        public BaseRepository<Student> StudentRepository => _repoStudent ?? (_repoStudent = new StudentRepository());

        private CourseService _courseService;
        public CourseService CourseService => _courseService ?? (_courseService = new CourseService(this));

        public void AddCourseToStudentByName(string studentIndex, string courseName)
        {
            var student = GetStudentByIndex(studentIndex);
            var course = _courseService.GetCourseByName(courseName);

            student.Courses.Add(new MongoDBRef("Courses",course.Id));
            UpdateStudent(student);
        }

        public List<Course> GetStudentCourses(string id)
        {
            List<Course> studentCourses = new List<Course>();
            var courses = _courseService.GetAllObjects();
            var student = GetStudentByIndex(id);
            foreach (var course in student.Courses)
            {
                var courseFound = courses.Find(x => x.ObjectId == course.Id);
                studentCourses.Add(courseFound);
            }
            return studentCourses;
        }

        public List<Student> GetObjectByFilter(FilterDefinition<Student> filter)
        {
            var objects = StudentRepository.GetFilteredCollection(filter).ToList();
            return objects;
        }

        public Student GetStudentByIndex(string index)
        {
            var objects = StudentRepository.GetObject(index);
            return objects;
        }

        public bool UpdateStudent(Student student)
        {
            var updateDefinition = Builders<Student>.Update
                 .Set(x => x.Name, student.Name)
                 .Set(x => x.Surname, student.Surname)
                 .Set(x => x.BirthDate, student.BirthDate)
                 .Set(x => x.Courses, student.Courses);

            var result = StudentRepository.Update(student.Id, updateDefinition);
            return result.IsAcknowledged;
        }

        public List<Student> GetAllObjects()
        {
            var collection = StudentRepository.GetCollection().AsQueryable().ToList();
            return collection;
        }

        public bool DeleteStudent(string index)
        {
            var result = StudentRepository.Delete(index);
            return result.DeletedCount != 0;
        }

        public void AddObject(Student value)
        {
            StudentRepository.AddObject(value);
        }
    }
}