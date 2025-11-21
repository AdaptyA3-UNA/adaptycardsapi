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
    [Route("api/decks")]
    [Authorize]
    public class DecksController : ControllerBase
    {
        // GET: api/decks
        [HttpGet]
        public IActionResult GetAllDecks()
        {
            // TODO: Buscar decks do usuário logado no banco
            return Ok(new[] { new { Id = 1, Name = "Inglês Básico" } });
        }

        // POST: api/decks
        [HttpPost]
        public IActionResult CreateDeck([FromBody] CreateDeckDto request)
        {
            // TODO: Salvar novo deck no banco
            return CreatedAtAction(nameof(GetAllDecks), new { id = 1 }, request);
        }

        // GET: api/decks/{deckId}/cards
        [HttpGet("{deckId}/cards")]
        public IActionResult GetCardsByDeck(int deckId)
        {
            // TODO: Buscar cartões onde DeckId == deckId
            return Ok(new[] { new { Id = 10, Front = "Hello", Back = "Olá" } });
        }

        // POST: api/decks/{deckId}/cards
        [HttpPost("{deckId}/cards")]
        public IActionResult AddCardToDeck(int deckId, [FromBody] CreateCardDto request)
        {
            // TODO: Salvar cartão vinculando ao deckId
            return Ok(new { message = $"Cartão adicionado ao deck {deckId}" });
        }
    }
}