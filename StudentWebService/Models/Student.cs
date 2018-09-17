using StudentWebService.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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

        //[DataMember]
        //[BsonRequired]
        public long Index
        {
            get => Convert.ToInt64(Id);
            set => Id =  value.ToString();
        }

        [DataMember]
        [BsonRequired]
        public string Name { get; set; }

        [DataMember]
        [BsonRequired]
        public string Surname { get; set; }

        [DataMember]
        [BsonRequired]
        public List<Course> Courses
        {
            get => _courses ?? new List<Course>();
            set => _courses = value;
        }

        [DataMember]
        [BsonRequired]
        private string Birthdate { get; set; }

        [XmlIgnore]
        [DataMember]
        private List<Course> _courses;

        [XmlIgnore]
        [IgnoreDataMember]
        public DateTime BirthDate
        {
            get => DateTime.ParseExact(Birthdate, "dd/MM/yyyy",CultureInfo.InvariantCulture);
            set => Birthdate = $"{value.Day.ToString().PadLeft(2,'0')}/{value.Month.ToString().PadLeft(2, '0')}/{value.Year}";
        }
        
    }
}