using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Security;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
using MongoDB.Driver.Core.Operations;
using StudentWebService.Helpers;
using StudentWebService.Models;
using StudentWebService.Models.Interfaces;

namespace StudentWebService.Repositories
{
    public abstract class BaseRepository<TModel> where TModel : IObject
    {
        private IMongoClient _mongoClient;
        public IMongoClient MongoClient => _mongoClient ??(_mongoClient = GetClient());

        private IMongoDatabase _mongoDb;
        public IMongoDatabase MongoDb => _mongoDb ?? (_mongoDb = GetMongoDb());

        private IMongoCollection<TModel> _collection;
        public readonly string CollectionName;

        protected BaseRepository(string collectionName)
        {
            CollectionName = collectionName;
            GetCollection();
        }
       
        private IMongoDatabase GetMongoDb()
        {
            return MongoClient.GetDatabase(Constants.DatabaseName);
        }

        private bool StartSession()
        {
            if (MongoClient.Cluster.Description.State == ClusterState.Connected)
                return true;
            MongoClient.StartSession();
            
            return MongoClient.Cluster.Description.State == ClusterState.Connected;
        }

        public IMongoClient GetClient()
        {
            return new MongoClient(Constants.ConnectionString);
        }

        public void DropCollection()
        {
            StartSession();
            MongoDb.DropCollection(CollectionName);
        }

        public virtual void CreateCollection()
        {
            StartSession();
            MongoDb.CreateCollection(CollectionName);
        }

        public IMongoCollection<TModel> GetCollection()
        {
            _collection = MongoDb.GetCollection<TModel>(CollectionName);
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