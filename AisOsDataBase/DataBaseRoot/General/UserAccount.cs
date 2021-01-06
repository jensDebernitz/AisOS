using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AisOsDataBase.DataBaseRoot.General
{
    public class UserAccount
    {
        public ObjectId Id { get; set; }
        public Person Person { get; set; }
        public Credential Credential { get; set; }
        public string Language { get; set; } = "en-US";
    }

    public class UserAccounts : BaseCollection<UserAccount>
    {
    }
}
