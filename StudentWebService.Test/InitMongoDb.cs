using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using StudentWebService.Models;
using StudentWebService.Repositories;

namespace StudentWebServiceConsole.Test
{
    public class InitMongoDb
    {
        readonly BaseRepository<Student> _repoStudent = new StudentRepository();
        readonly BaseRepository<Course> _repoCourse = new CourseRepository();
        readonly BaseRepository<Mark> _repoMark = new MarkRepository();

        private static List<Course> CourseList = new List<Course>()
        {
            new Course()
            {
                ObjectId= ObjectId.GenerateNewId(),
                LeadTeacher = @"Teacher A",
                CourseName = @"Javascript Course",
                Points = "10"
            },
            new Course()
            {
                ObjectId= ObjectId.GenerateNewId(),
                LeadTeacher = @"Teacher A",
                CourseName = @"HTML Course",
                Points = "10"
            },
            new Course()
            {
                ObjectId= ObjectId.GenerateNewId(),
                LeadTeacher = @"Teacher A",
                CourseName = @"CSS Course",
                Points = "3"
            },
            new Course()
            {
                ObjectId= ObjectId.GenerateNewId(),
                LeadTeacher = @"Teacher A",
                CourseName = @"C# Course",
                Points = "10"
            }
        };

        private List<Student> listStudent = new List<Student>()
        {
            new Student()
            {
                Index  = 12345,
                BirthDate = DateTime.Today,
                Name = "Imie",
                Surname = "Surname"
            },
            new Student()
            {
                Index   = 1234,
                BirthDate = DateTime.Today,
                Name = "Imie",
                Surname = "Surname"
            },
        };


        public void CreateCollections()
        {
            //cerate uniquieindex for collection
            CreateUniqueStudentIndex();

            //add element to collections Student
            listStudent.ForEach(item=> _repoStudent.AddObject(item));
        }

        private void CreateUniqueStudentIndex()
        {
            var collection = _repoStudent.GetCollection();
            collection.Indexes.CreateOne(new CreateIndexModel<Student>(Builders<Student>.IndexKeys.Ascending(i => i.Index), new CreateIndexOptions<Student>
            {
                Unique = true
            }));
        }
    }
}