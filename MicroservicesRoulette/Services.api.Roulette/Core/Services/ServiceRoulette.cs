using MongoDB.Driver;
using Services.api.Roulette.Core.Entities;
using Services.api.Roulette.Core.Models;
using Services.api.Roulette.Core.Repository;
using Services.api.Roulette.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.api.Roulette.Core.Services
{
    public class ServiceRoulette : IServiceRoulette
    {
        private readonly IMongoRepository<RouletteEntity> _rouletteRepository;
        private readonly IMongoRepository<BetEntity> _betRepository;
        private Utilities _utilities;

        public ServiceRoulette(IMongoRepository<RouletteEntity> rouletteRepository, IMongoRepository<BetEntity> betRepository)
        {
            _rouletteRepository = rouletteRepository;
            _betRepository = betRepository;
            _utilities = new Utilities();
        }
        public async Task<BetEntity> Bet(string idRoulette, string userId, BetModel betRquest)
        {            
            string message = string.Empty;                
            RouletteEntity roulette = await _rouletteRepository.GetById(idRoulette);
            message = _utilities.ValidateRoulette(roulette, isbetting: true);
            if (message != "")
            {
                throw new Exception(message);
            }
            message = _utilities.ValidateBetRequest(betRquest);
            if (message != "")
            {
                throw new Exception(message);
            }
            BetEntity bet = new BetEntity()
            {
                BetDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'"),
                UserId = userId,
                BetModel = betRquest,
                Winner = false,
                EarnedMoney = 0,
                Roulette = roulette
            };
            await _betRepository.InsertDocument(bet);

            return bet;
        }
        public async Task<IEnumerable<BetEntity>> Close(string id)
        {
            RouletteEntity roulette = await _rouletteRepository.GetById(id);
            string message = _utilities.ValidateRoulette(roulette, isClosing: true);
            if (message != "")
            {
                throw new Exception(message);
            }
            var winningNumber = _utilities.GetWinningNumber();
            var winningColor = (_utilities.ValidateEvenNumber(winningNumber)) ? "rojo" : "negro";
            roulette.ClosingDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");
            roulette.WinningNumber = winningNumber;
            roulette.WinningColor = winningColor;
            roulette.State = "closed";
            await _rouletteRepository.UpdateDocument(roulette);            
            var listBet = await _betRepository.GetByFilter(Builders<BetEntity>.Filter.Eq(doc => doc.Roulette.Id, id));
            foreach (var bet in listBet)
            {
                bet.Roulette = roulette;
                if (bet.BetModel.BetType.ToLower() == "color")
                {
                    if(bet.BetModel.BetValue.ToLower() == winningColor)
                    {
                        bet.Winner = true;
                        bet.EarnedMoney = (bet.BetModel.BetMoney * 1.8);
                    }                    
                }
                else
                {
                    if (Convert.ToInt32(bet.BetModel.BetValue) == winningNumber)
                    {
                        bet.Winner = true;
                        bet.EarnedMoney = (bet.BetModel.BetMoney * 5);
                    }
                }
                await _betRepository.UpdateDocument(bet);
            }

            return listBet;
        }
        public async Task<RouletteEntity> Create()
        {
            RouletteEntity roulette = new RouletteEntity()
            {            
                State = "create",
                OpeningDate = null,
                ClosingDate = null,
                WinningNumber = -1,
                WinningColor = ""
            };
            await _rouletteRepository.InsertDocument(roulette);

            return roulette;
        }
        public async Task<RouletteEntity> Open(string Id)
        {
            RouletteEntity roulette = await _rouletteRepository.GetById(Id);
            string message = _utilities.ValidateRoulette(roulette, isOpening: true);
            if (message != "")
            {
                throw new Exception(message);
            }            
            roulette.OpeningDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");            
            roulette.State = "open";
            await _rouletteRepository.UpdateDocument(roulette);

            return roulette;
        }
        public async Task<IEnumerable<RouletteEntity>> GetAll() => await _rouletteRepository.GetAll();

    }
}
