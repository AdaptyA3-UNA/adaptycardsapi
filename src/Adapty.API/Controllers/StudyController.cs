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
            // request.Quality deve ser: 0 (Difícil), 3 (Médio), 5 (Fácil), etc.
            
            // TODO: Aplicar o algoritmo SM-2 (Repetição Espaçada)
            // Calcular próxima data de revisão baseada na 'request.Quality'
            
            return Ok(new { message = "Revisão registrada", nextReview = DateTime.Now.AddDays(1) });
        }
    }
}