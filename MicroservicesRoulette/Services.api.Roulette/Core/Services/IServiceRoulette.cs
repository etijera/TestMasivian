using Services.api.Roulette.Core.Entities;
using Services.api.Roulette.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.api.Roulette.Core.Services
{
    public interface IServiceRoulette
    {
        Task<RouletteEntity> Create();
        Task<RouletteEntity> Open(string Id);
        Task<IEnumerable<BetEntity>> Close(string Id);
        Task<BetEntity> Bet(string idRoulette, string userId, BetModel betRequest);
        Task<IEnumerable<RouletteEntity>> GetAll();
    }
}
