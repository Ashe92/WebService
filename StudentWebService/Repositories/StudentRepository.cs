using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using StudentWebService.Repositories;
using StudentWebService.Helpers;
using StudentWebService.Models;

namespace StudentWebService.Repositories
{
    public class StudentRepository : BaseRepository<Student>
    {
        public StudentRepository() : base("Student")
        {

        }

        public override void CreateCollection()
        {
            MongoDb.CreateCollection(CollectionName);
            GetCollection().Indexes.CreateOne(new CreateIndexModel<Student>(Builders<Student>.IndexKeys.Ascending(_ => _.Index)));

        }
    }


}