using System;
using System.Collections.Generic;
using System.Text;
using AisOsDataBase.DataBaseRoot.General;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AisOsDataBase.DataBaseRoot.Patient
{
    public class Patient
    {
        public ObjectId Id { get; set; }
        public Person Person { get; set; }
    }

    public class Patients : BaseCollection<Patient>
    {

    }
}
