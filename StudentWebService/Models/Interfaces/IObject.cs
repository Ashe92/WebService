using System.Runtime.Serialization;
using System.Xml.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentWebService.Models.Interfaces
{
    public interface IObject 
    {
        ObjectId ObjectId { get; set; }

        string Id { get; set; }
    }
}