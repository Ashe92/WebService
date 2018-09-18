using StudentWebService.Helpers;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StudentWebService.Enums;
using StudentWebService.Models.Interfaces;

namespace StudentWebService.Models
{
    public class Mark : IObject
    {
        [BsonId]
        [XmlIgnore]
        [IgnoreDataMember]
        public ObjectId ObjectId { get; set; }

        public string StudentId { get; set;}
        public string CourseId { get; set; }

        public string Id
        {
            get => $"{EvaluationType},{StudentId},{CourseId}";
            set => Id = value;
        }

        [BsonDateTimeOptions]
        public DateTime AddedDate { get; set; }

        public decimal Evaliation
        {
            get;
            set;
        }

        [XmlIgnore]
        [BsonIgnore]
        public MarkValuesEnum EvaluationType
        {
            get => MarkValue.GetValue(Evaliation);
            set => Evaliation = MarkValue.GetValue(value);
        }
    }
}