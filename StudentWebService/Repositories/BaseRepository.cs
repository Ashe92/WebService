using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Security;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
using StudentWebService.Helpers;
using StudentWebService.Models;
using StudentWebService.Models.Interfaces;

namespace StudentWebService.Repositories
{
    public abstract class BaseRepository<TModel> where TModel : IObject
    {
        private IMongoClient _mongoClient;
        public IMongoClient MongoClient  => _mongoClient == null ? _mongoClient :(_mongoClient = new  MongoClient(Constants.ConnectionString));

        private IMongoDatabase _mongoDb;
        public IMongoDatabase MongoDb => _mongoDb == null ? _mongoDb : (_mongoDb = MongoClient.GetDatabase(Constants.DatabaseName));

        private IMongoCollection<TModel> _collection;
        private readonly string _collectionName;

        protected BaseRepository(string collectionName)
        {
            _collectionName = collectionName;
            GetCollection();
        }

        private bool StartSession()
        {
            if (MongoClient.Cluster.Description.State == ClusterState.Connected)
                return true;
            MongoClient.StartSession();

            return MongoClient.Cluster.Description.State == ClusterState.Connected;
        }
        

        public IMongoCollection<TModel> GetCollection()
        {
            _collection = MongoDb.GetCollection<TModel>(_collectionName);
            return _collection;
        }

        public TModel GetObject(string id)
        {
            StartSession();
            return _collection.Find(item => item.Id == id).SingleOrDefault();
        }

        public IEnumerable<TModel> GetFilteredCollection(FilterDefinition<TModel> filter)
        {
            StartSession();
            return _collection.Find(filter).ToList(); 
        }

        public UpdateResult Update(string id, UpdateDefinition<TModel> updateObject )
        {
            StartSession();
            return _collection.UpdateOne(item => item.Id == id, updateObject);
        }

        public DeleteResult Delete(string id)
        {
            StartSession();
            return _collection.DeleteOne(item => item.Id == id);
        }

        public void AddObject(TModel addElement)
        {
            StartSession();
            _collection.InsertOne(addElement);
        }
    }
}