using MongoDB.Bson.Serialization.Attributes;
using Services.api.Roulette.Core.Models;

namespace Services.api.Roulette.Core.Entities
{
    [BsonCollection("Bet")]
    public class BetEntity : Document
    {
        [BsonElement("betDate")]
        public string BetDate { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("betModel")]
        public BetModel BetModel { get; set; }

        [BsonElement("winner")]
        public bool Winner { get; set; }

        [BsonElement("earnedMoney")]
        public double EarnedMoney { get; set; }

        [BsonElement("roulette")]
        public RouletteEntity Roulette { get; set; }
    }
}
