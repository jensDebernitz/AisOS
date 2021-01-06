using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AisOsDataBase.DataBaseRoot.General
{
    public class Credential
    {
        public ObjectId Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Credentials : BaseCollection<Credential>
    {

    }
}
