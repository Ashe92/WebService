using StudentWebService.Helpers;
using StudentWebService.Models.Interfaces;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
            get => Id;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                Id = $"{Evaluation},{StudentId},{CourseId}";
            }
        }

        public DateTime AddedDate { get; set; }

        private decimal _evaluation
        {
            get;
            set;
        }

        [XmlIgnore]
        [IgnoreDataMember]
        public MarkValuesEnum Evaluation
        {
            get => MarkValue.GetValue(_evaluation);
            set => _evaluation = MarkValue.GetValue(value);
        }
    }
}