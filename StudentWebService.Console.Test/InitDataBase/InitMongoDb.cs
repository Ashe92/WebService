using System;
using System.Collections.Generic;
using MongoDB.Bson;
using StudentWebService.Helpers;
using StudentWebService.Models;
using StudentWebService.Repositories;

namespace StudentWebService.Console.Test.InitDataBase
{
    public class InitMongoDb
    {
        private readonly BaseRepository<Student> _repoStudent  = new StudentRepository();
        private readonly BaseRepository<Course> _repoCourse = new CourseRepository();
        private readonly BaseRepository<Mark> _repoMark = new MarkRepository();

        private List<Course> CourseList = new List<Course>()
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

        private List<Student> StudentList = new List<Student>()
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

        private List<Mark> MarksList = new List<Mark>()
        {
            new Mark()
            {
                AddedDate = DateTime.Today,
                EvaluationType = MarkValuesEnum.Three,
                StudentId = "12345",
                CourseId = "CSS Course"
            }
        };

        public void CreateCollections()
        {
            //Resfersh collections
            RefreshDataBase();
            var test = _repoStudent.GetCollection();
            //add element to collections Student
            StudentList.ForEach(item=> _repoStudent.AddObject(item));

            //add element to collections Student
            CourseList.ForEach(item => _repoCourse.AddObject(item));

            MarksList.ForEach(item => _repoMark.AddObject(item));
        }

        private void RefreshDataBase()
        {
            _repoStudent.DropCollection();
            _repoCourse.DropCollection();
            _repoMark.DropCollection();
            _repoStudent.CreateCollection();
            _repoCourse.CreateCollection();
            _repoMark.CreateCollection();
        }
    }
}