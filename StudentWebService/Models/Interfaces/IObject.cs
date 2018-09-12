using MongoDB.Bson;

namespace StudentWebService.Models.Interfaces
{
    public interface IObject 
    {
        ObjectId ObjectId { get; set; }

        string Id { get; set; }
    }
}