using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace AisOsDataBase.DataBaseRoot.General
{
    public class Person
    {
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
