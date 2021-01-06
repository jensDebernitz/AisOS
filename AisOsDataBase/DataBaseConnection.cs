using System;
using AisOsDataBase.DataBaseRoot.General;
using AisOsDataBase.DataBaseRoot.Patient;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AisOsDataBase
{
    public class DataBaseConnection
    {
        private static DataBaseConnection _instance = null;

        public static DataBaseConnection Instance()
        {
            return _instance ??= new DataBaseConnection();
        }

        private DataBaseConnection()
        {

        }

        public MongoClient MongoClient { get; private set; }
        public IMongoDatabase MongoDataBaseRoot { get; private set; }
        public IMongoDatabase MongoDataBaseCustom { get; private set; }

        public void Init(string url = "mongodb://localhost")
        {
            var connectionString = url;
            MongoClient = new MongoClient(connectionString);

            if (MongoClient != null)
            {
                MongoDataBaseRoot = MongoClient.GetDatabase("AisOsRoot");
                MongoDataBaseCustom = MongoClient.GetDatabase("AisOsCustom");

                MongoDataBaseCustom.GetCollection<Patient>("patients");
            }
        }
    }
}
