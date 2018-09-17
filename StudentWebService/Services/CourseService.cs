using StudentWebService.Models;
using StudentWebService.Repositories;
using System;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace StudentWebService.Services
{
    public class CourseService
    {
        private readonly BaseRepository<Course> _repoCourse =new CourseRepository();

        public Course GetCourseByName(string name)
        {
            var objects = _repoCourse.GetObject(name);
            return objects ?? throw new Exception($"Brak kursu o nazwie: {name}");
        }

        public bool UpdateCourse(Course course)
        {
            var updateDefinition = Builders<Course>.Update
                 .Set(x => x.LeadTeacher, course.LeadTeacher)
                 .Set(x => x.Points, course.Points);

            var result = _repoCourse.Update(course.Id, updateDefinition);
            return result.ModifiedCount != 0;
        }

        public IMongoQueryable<Course> GetAllCourses()
        {
            var collection = _repoCourse.GetCollection().AsQueryable();
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