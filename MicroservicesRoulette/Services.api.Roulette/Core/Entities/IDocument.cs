using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.api.Roulette.Core.Entities
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        string Id { get; set; }

        string CreatedDate { get; }
    }
}
