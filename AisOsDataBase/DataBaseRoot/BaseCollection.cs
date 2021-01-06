using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AisOsDataBase.DataBaseRoot
{
    public class BaseCollection<T>
    {
        public IMongoCollection<T> GetCollection()
        {
            return DataBaseConnection.Instance().MongoDataBaseCustom
                .GetCollection<T>(typeof(T).ToString());
        }

        public bool CollectionExists()
        {
            var filter = new BsonDocument("name", typeof(T).ToString());
            var options = new ListCollectionNamesOptions { Filter = filter };

            return DataBaseConnection.Instance().MongoDataBaseCustom.ListCollectionNames(options).Any();
        }
    }
}
