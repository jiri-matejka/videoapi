using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoApi.Data.Entities
{
    public class Account
    {
        public string Id { get; set; }
        
        public ResumePoint[] ResumePoints { get; set; }
    }
}
