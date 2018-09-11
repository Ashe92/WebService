using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using StudentWebService.Repositories;
using StudentWebService.Helpers;
using StudentWebService.Models;

namespace StudentWebService.Repositories
{
    public class StudentRepository : BaseRepository<Student>
    {
        public StudentRepository() : base("Student")
        {

        }
    }


}