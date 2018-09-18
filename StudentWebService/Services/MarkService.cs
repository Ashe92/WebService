using MongoDB.Bson;
using MongoDB.Driver;
using StudentWebService.Models;
using StudentWebService.Repositories;
using StudentWebService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentWebService.Services
{
    public class MarkService : IObjectService
    {
        private readonly BaseRepository<Mark> _repoMark = new MarkRepository();

        public Mark GetObjectById(string Id)
        {
            var objects = _repoMark.GetObject(Id);
            return objects ?? throw new Exception($"Brak oceny o id: {Id}");
        }

        public List<Mark> GetObjectByFilter(FilterDefinition<Mark> filter)
        {
            var objects = _repoMark.GetFilteredCollection(filter).ToList();
            return objects.Count == 0 ? throw new Exception($"Brak oceny o danych zmiennych: {filter.ToJson()}") : objects;
        }

        public bool UpdateObject(Mark mark)
        {
            var updateDefinition = Builders<Mark>.Update
                .Set(x => x.EvaluationType, mark.EvaluationType);

            var result = _repoMark.Update(mark.Id, updateDefinition);
            return result.ModifiedCount != 0;
        }

        public List<Mark> GetAllObjects()
        {
            var collection = _repoMark.GetCollection().AsQueryable().ToList<Mark>();
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