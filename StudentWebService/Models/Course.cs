using System.Collections.Generic;

namespace StudentWebService.Models
{
    public class Course
    {
        public string CourseName { get; set; }
        public string LeadTeacher { get; set; }
        public string StudentId { get; set; }
        public List<Mark> ListMarks { get; set; }
    }
}