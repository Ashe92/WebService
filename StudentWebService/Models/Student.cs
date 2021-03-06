﻿using System;
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
        
        [BsonIgnore]
        public long Index
        {
            get => Convert.ToInt64(Id);
            set => Id =  value.ToString();
        }
        
        [BsonRequired]
        public string Name { get; set; }


        public string Surname { get; set; }

        [DataMember]
        public List<MongoDBRef> Courses
        {
            get => _courses ?? new List<MongoDBRef>();
            set => _courses = value;
        }

        private List<MongoDBRef> _courses;

        [BsonDateTimeOptions]
        public DateTime BirthDate { get; set; }


    }
}