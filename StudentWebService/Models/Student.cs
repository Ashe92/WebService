using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using StudentWebService.Models.Interfaces;

namespace StudentWebService.Models
{
    public class Student : IObject
    {
        [BsonId]
        [XmlIgnore]
        [IgnoreDataMember]
        public ObjectId ObjectId { get; set; }

        public string Id
        {
            get;
            set;
        }


        public long Index
        {
            get => Convert.ToInt64(Id);
            set => Id =  value.ToString();
        }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<Course> Courses
        {
            get => _courses ?? new List<Course>();
            set => _courses = value;
        }


        private string Birthdate {get;set;}

        [XmlIgnore]
        [IgnoreDataMember]
        private List<Course> _courses;

        [XmlIgnore]
        [IgnoreDataMember]
        public DateTime BirthDate
        {
            get => DateTime.ParseExact(Birthdate, "dd/MM/yyyy",CultureInfo.InvariantCulture); 
            set => Birthdate = value.ToString(CultureInfo.InvariantCulture);
        }



    }
}