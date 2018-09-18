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
    public class StudentService : IObjectService
    {
        private readonly BaseRepository<Student> _repoStudent = new StudentRepository();
        private readonly CourseService _courseService = new CourseService();

        public void AddCourseToStudentByName(string studentIndex, string courseName)
        {
            var student = GetStudentByIndex(studentIndex);
            var course = _courseService.GetCourseByName(courseName);

            student.Courses.Add(course);
            UpdateStudent(student);
        }

        public List<Student> GetObjectByFilter(FilterDefinition<Student> filter)
        {
            var objects = _repoStudent.GetFilteredCollection(filter).ToList();
            return objects.Count == 0 ?throw new Exception($"Brak Studenta o danych zmiennych: {filter.ToJson()}") : objects;
        }

        public Student GetStudentByIndex(string index)
        {
            var objects = _repoStudent.GetObject(index);
            return objects ?? throw new Exception($"Brak Studenta o indexie: {index}");
        }

        public bool UpdateStudent(Student student)
        {
            var updateDefinition = Builders<Student>.Update
                 .Set(x => x.Name, student.Name)
                 .Set(x => x.Surname, student.Surname)
                 .Set(x => x.BirthDate, student.BirthDate)
                 .Set(x => x.Courses, student.Courses);

            var result = _repoStudent.Update(student.Id, updateDefinition);
            return result.IsAcknowledged;
        }

        public List<Student> GetAllObjects()
        {
            var collection = _repoStudent.GetCollection().AsQueryable().ToList();
            return collection;
        }

        public bool DeleteStudent(string index)
        {
            var result = _repoStudent.Delete(index);
            return result.DeletedCount != 0;
        }

        public void AddObject(Student value)
        {
            _repoStudent.AddObject(value);
        }
    }
}