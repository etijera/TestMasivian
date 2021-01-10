using MongoDB.Bson.Serialization.Attributes;

namespace Services.api.Roulette.Core.Entities
{
    [BsonCollection("Roulette")]
    public class RouletteEntity : Document
    {
        [BsonElement("state")]
        public string State { get; set; }

        [BsonElement("openingDate")]
        public string OpeningDate { get; set; }

        [BsonElement("closingDate")]
        public string ClosingDate { get; set; }

        [BsonElement("winningNumber")]
        public int WinningNumber { get; set; }

        [BsonElement("winningColor")]
        public string WinningColor { get; set; }
    }
}
