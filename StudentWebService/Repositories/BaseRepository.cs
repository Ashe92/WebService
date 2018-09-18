using StudentWebService.Helpers;
using StudentWebService.Models.Interfaces;
using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;

namespace StudentWebService.Repositories
{
    public abstract class BaseRepository<TModel> where TModel : IObject
    {
        private IMongoClient _mongoClient;
        public IMongoClient MongoClient => _mongoClient ?? (_mongoClient = GetClient());

        

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
            //if (MongoClient.Cluster.Description.State == ClusterState.Disconnected)
              //  throw new Exception($"Brak połączenia do bazy danych");
            if (MongoClient.Cluster.Description.State == ClusterState.Connected)
                return true;
            MongoClient.StartSession();
            
            return MongoClient.Cluster.Description.State == ClusterState.Connected;
        }

        public IMongoClient GetClient()
        {
            try
            {
                return new MongoClient(Constants.ConnectionString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public UpdateResult Update(string id, UpdateDefinition<TModel> updateObject)
        {
            StartSession();
            var result = _collection.UpdateOne(item => item.Id == id, updateObject);
            GetCollection();
            return result;
        }

        public DeleteResult Delete(string id)
        {
            StartSession();
            var result = _collection.DeleteOne(item => item.Id == id);
            GetCollection();
            return result;
        }

        public void AddObject(TModel addElement)
        {
            StartSession();
            _collection.InsertOne(addElement);
            GetCollection();
        }
    }
}