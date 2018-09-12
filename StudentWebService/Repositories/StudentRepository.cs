using MongoDB.Driver;
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