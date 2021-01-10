using System;

namespace Services.api.Roulette.Core.Models
{
    public class BetModel
    {        
        public string BetType { get; set; }
        public string BetValue { get; set; }        
        public double BetMoney { get; set; }
    }
}
