using System;
using System.Collections.Generic;
using System.Text;
using AisOsDataBase.DataBaseRoot.General;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AisOsDataBase.DataBaseRoot.Configuration
{
    public class Configuration
    {
        public ObjectId Id { get; set; }
        public UserAccount LastUserLogin { get; set; }
    }

    public class Configurations : BaseCollection<Configuration>
    {

    }
}
