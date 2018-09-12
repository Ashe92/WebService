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


        public void CreateCollections()
        {
            //Resfersh collections
            RefreshDataBase();

            //add element to collections Student
            //StudentList.ForEach(item=> _repoStudent.AddObject(item));


            //add element to collections Student
           // CourseList.ForEach(item => _repoCourse.AddObject(item));
        }

        private void RefreshDataBase()
        {
            //_repoStudent.DropCollection();
            //_repoCourse.DropCollection();
            //_repoMark.DropCollection();
            //_repoStudent.CreateCollection();
            //_repoCourse.CreateCollection();
            //_repoMark.CreateCollection();
        }
    }
}