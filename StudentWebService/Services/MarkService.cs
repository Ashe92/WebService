using System;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using StudentWebService.Models;
using StudentWebService.Repositories;
using StudentWebService.Services.Interfaces;

namespace StudentWebService.Services
{
    public class MarkService : IObjectService
    {
        private readonly BaseRepository<Mark> _repoMark = new MarkRepository();

        public Mark GetObjectByName(string name)
        {
            var objects = _repoMark.GetObject(name);
            return objects ?? throw new Exception($"Brak kursu o nazwie: {name}");
        }

        public bool UpdateObject(Mark mark)
        {
            var updateDefinition = Builders<Mark>.Update
                .Set(x => x.Evaluation, mark.Evaluation);

            var result = _repoMark.Update(mark.Id, updateDefinition);
            return result.ModifiedCount != 0;
        }

        public IMongoQueryable<Mark> GetAllObjects()
        {
            var collection = _repoMark.GetCollection().AsQueryable();
            return collection;
        }

        public bool DeleteObject(string name)
        {
            var result = _repoMark.Delete(name);
            return result.DeletedCount != 0;
        }

        public void AddObject(Mark value)
        {
            _repoMark.AddObject(value);
        }
    }
}