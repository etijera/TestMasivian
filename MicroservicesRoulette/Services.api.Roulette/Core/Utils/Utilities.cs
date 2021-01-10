using Services.api.Roulette.Core.Entities;
using Services.api.Roulette.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.api.Roulette.Core.Utils
{
    public class Utilities
    {
        public Utilities(){}
        public string ValidateBetRequest(BetModel betRquest)
        {
            decimal value; string message = string.Empty;
            if (betRquest.BetMoney > 10000 || betRquest.BetMoney < 1)
            {
                message = "Tipo de apuesta inválida. El BetMoney debe estar entre 1 y 10000.";
            }
            if ((betRquest.BetType.ToLower() != "color") && (betRquest.BetType.ToLower() != "number"))
            {
                message = "Tipo de apuesta inválida. El BetType debe ser number o color.";
            }
            else
            {
                if (betRquest.BetType.ToLower() == "color")
                {
                    if (decimal.TryParse(betRquest.BetValue, out value))
                    {
                        message = "Tipo de apuesta inválida. Si el BetType es color el BetValue debe ser negro o rojo.";
                    }
                    else
                    {
                        if (betRquest.BetValue.ToLower() != "negro" && betRquest.BetValue.ToLower() != "rojo")
                        {
                            message = "Tipo de apuesta inválida. Si el BetType es color el BetValue debe ser negro o rojo.";
                        }
                    }
                }
                else
                {
                    if (decimal.TryParse(betRquest.BetValue, out value))
                    {
                        if (value > 36 || value < 0)
                        {
                            message = "Tipo de apuesta inválida. Si el BetType es number el BetValue debe estar en el rango de 0 a 36.";
                        }
                    }
                    else
                    {
                        message = "Tipo de apuesta inválida. Si el BetType es number el BetValue debe estar en el rango de 0 a 36.";
                    }
                }
            }

            return message;
        }
        public string ValidateRoulette(RouletteEntity roulette, bool isbetting = false, bool isClosing = false, bool isOpening = false)
        {
            string message = string.Empty;
            if (roulette == null)
            {
                message = "La ruleta no existe.";
            }
            else
            {
                if (roulette.State == "create" && (isbetting || isClosing))
                {
                    message = "La ruleta aún no se encuentra abierta.";
                }
                if (roulette.State == "open" && (isOpening))
                {
                    message = "La ruleta ya se encuentra abierta.";
                }
                if (roulette.State == "closed" && (isOpening || isbetting || isClosing))
                {
                    message = "La ruleta ya fue cerrada.";
                }
            }
            
            return message;
        }
        public int GetWinningNumber() => new Random(Environment.TickCount).Next(0, 36);
        public bool ValidateEvenNumber(int number) => (number % 2 == 0);

    }
}
