using System;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using StudentWebService.Models;
using StudentWebService.Repositories;
using StudentWebService.Services.Interfaces;

namespace StudentWebService.Services
{
    public class StudentService : IObjectService
    {
        private readonly BaseRepository<Student> _repoStudent = new StudentRepository();
        private readonly CourseService _courseService = new CourseService();

        public void AddCourseToStudentByName(long studentIndex, string CourseName)
        {
            var student = GetStudentByIndex(studentIndex);
            var course = _courseService.GetCourseByName(CourseName);

            student.Courses.Add(course);
            UpdateStudent(student);
        }

        public Student GetStudentByIndex(long index)
        {
            var builder = Builders<Student>.Filter;
            var filter = builder.Eq(item => item.Index, index);
            var objects = _repoStudent.GetObjectByFilter(filter);
            return objects ?? throw new Exception($"Brak Studenta o indexie: {index}");
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
            return result.ModifiedCount != 0;
        }

        public IMongoQueryable<Student> GetAllStudents()
        {
            var collection = _repoStudent.GetCollection().AsQueryable();
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