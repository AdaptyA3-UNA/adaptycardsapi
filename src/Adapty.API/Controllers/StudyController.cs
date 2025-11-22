using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Adapty.API.DTOs;


namespace Adapty.API.Controllers
{
    [ApiController]
    [Route("api/study")]
    [Authorize]
    public class StudyController : ControllerBase
    {
        // POST: api/study/session
        [HttpPost("session")]
        public IActionResult StartSession([FromBody] StartSessionDto request)
        {
            // TODO: Algoritmo para selecionar quais cartões o usuário deve estudar agora
            return Ok(new { sessionId = "sessao_123", cardsToReview = 10 });
        }

        // PUT: api/study/card/{cardId}/review
        [HttpPut("card/{cardId}/review")]
        public IActionResult ReviewCard(int cardId, [FromBody] ReviewCardDto request)
        {
            // 1. Buscar o cartão no banco pelo cardId (simulado aqui)
            // var card = _context.Cards.Find(cardId);
    
            // 2. Aplicar lógica simplificada do SM-2
            // Se a qualidade (request.Quality) for >= 3 (Acertou):
            //    - Atualiza o Intervalo (ex: de 1 dia vai para 6 dias)
            //    - Atualiza o EaseFactor
            // Se a qualidade for < 3 (Errou):
            //    - Reseta o Intervalo para 1 dia

            // 3. Calcular nova data
            // card.NextReviewDate = DateTime.Now.AddDays(card.IntervalInDays);

            // 4. Salvar no banco e retornar
            return Ok(new { message = "Revisão registrada", nextReview = DateTime.Now.AddDays(1) });
        }
    }
}