using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.api.Roulette.Core.Entities;
using Services.api.Roulette.Core.Models;
using Services.api.Roulette.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.api.Roulette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        private readonly IServiceRoulette _rouletteService;

        public RouletteController(IServiceRoulette rouletteRepository)
        {
            _rouletteService = rouletteRepository;
        }

        [HttpPost]
        public async Task<IActionResult> NewRulette()
        {
            RouletteEntity roulette = await _rouletteService.Create();

            return Ok( new { Id =  roulette.Id });
        }

        [HttpPut("open/{id}")]
        public async Task<IActionResult> OpenRulette(string id)
        {
            try
            {
                RouletteEntity roulette = await _rouletteService.Open(id);

                if (roulette.State == "open")
                {
                    return Ok(new { state = "Operación exitosa.", errorMessge = "" });
                }
                else
                {
                    return Ok(new { state = "Operación denegada.", errorMessge = "No se abrió la ruleta." });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
                return Ok(new { state = "Operación denegada.", errorMessge = e.Message });
            }
        }

        [HttpPost("bet/{idRoulette}")]
        public async Task<IActionResult> BetRulette([FromHeader(Name = "userId")] string UserId, [FromRoute(Name = "idRoulette")] string idRoulette, [FromBody] BetModel request)
        {
            try
            {
                BetEntity betRoulette = await _rouletteService.Bet(idRoulette, UserId, request);

                return Ok(betRoulette);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return Ok(new { state = "Error.", errorMessge = e.Message });
            }
        }

        [HttpPut("close/{id}")]
        public async Task<IActionResult> CloseRulette(string id)
        {
            try
            {
                var betsRoulette = await _rouletteService.Close(id);

                return Ok(betsRoulette);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return Ok(new { state = "Error.", errorMessge = e.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouletteEntity>>> GetRoulettes()
        {
            var roulettes = await _rouletteService.GetAll();

            return Ok(roulettes);
        }
    }
}
