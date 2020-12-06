using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TDM.Domain.Models
{
    public class DomainObject : ObservableObject
    {
        [BsonIgnore]
        public int Id { get; set; }
    }
}
