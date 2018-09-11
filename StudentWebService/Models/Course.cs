using System.Collections.Generic;
using MongoDB.Bson;
using StudentWebService.Models.Interfaces;

namespace StudentWebService.Models
{
    public class Course : IObject
    {
        public ObjectId ObjectId { get; set; }
        public string Id { get; set; }
        public string CourseName { get; set; }
        public string LeadTeacher { get; set; }
        public string Points { get; set; }
    }
}