using System;

namespace StudentWebService.Models
{
    public class Mark
    {
        public string MarkId { get => $"{StudentId},{CourseName},{Markvalue}"; set => MarkId = value; }

        public float Markvalue { get; set; }
        public DateTime AddedDate { get; set; }

        public string CourseName { get; set; }
        public string StudentId { get; set; }
    }
}