using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Services.api.Roulette.Core.Entities
{
    public class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string CreatedDate => DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");
    }
}
