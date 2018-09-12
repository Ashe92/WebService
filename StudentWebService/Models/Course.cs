using MongoDB.Bson;
using StudentWebService.Models.Interfaces;

namespace StudentWebService.Models
{
    public class Course : IObject
    {
        public ObjectId ObjectId { get; set; }
        public string Id { get; set; }

        public string CourseName
        {
            get => Id;
            set => Id = value;
        }

        public string LeadTeacher { get; set; }

        public string Points { get; set; }
    }
}