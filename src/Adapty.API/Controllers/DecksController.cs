using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Adapty.API.DTOs;
using Adapty.API.Models;
using Adapty.API.Services;
using Adapty.API.Data;

namespace Adapty.API.Controllers
{
    [ApiController]
    [Route("api/decks")]
    [Authorize]
    public class DecksController : ControllerBase
    {
        private readonly DeckService _deckService;
        private readonly CardService _cardService;

        private readonly AppDbContext _context;

        public DecksController(DeckService deckService, CardService cardService, AppDbContext context)
        {
            _deckService = deckService;
            _cardService = cardService;
            _context = context;
        }

        // 1. LISTAR TODOS OS DECKS
        [HttpGet]
        public IActionResult GetAllDecks()
        {
            var decks = _deckService.GetAllDecks();
            if (decks == null || !decks.Any())
            {
                return NotFound("Nenhum deck encontrado.");
            }
            
            return Ok(decks);
        }

        // 2. CRIAR UM NOVO DECK
        [HttpPost]
        public IActionResult CreateDeck([FromBody] CreateDeckDto request)
        {            
            var deck = new Deck
            {
                Id = request.Id,
                Nome = request.Title,
                Descricao = request.Description
            };
            
            _deckService.CreateDeck(deck);
            
            var deckDto = new DeckDto(
                deck.Id,
                deck.Nome,
                deck.Descricao,
                new string[] {}
            );

            return CreatedAtAction(nameof(GetCardsByDeck), new { deckId = deck.Id }, deckDto);
        }

        // 3. BUSCAR CARTAS DE UM DECK ESPECÍFICO (GET)
        [HttpGet("{deckId}/cards")]
        public IActionResult GetCardsByDeck([FromBody] int deckId)
        {
            var cards = _cardService.GetCardsByDeck(deckId);

            var cardsDto = cards.Select(c => new CardDto(
                c.Id, 
                c.Pergunta, 
                c.Resposta
                )).ToList();

            return Ok(cards);
        }

        // 4. ADICIONAR CARTA EM UM DECK (POST)
        [HttpPost("{deckId}/cards")]
        public IActionResult AddCardToDeck(int deckId, [FromBody] CreateCardDto request)
        {
            var cards = _cardService.GetCardsByDeck(deckId);

            var card = new Card
            {
                Pergunta = request.FrontText,
                Resposta = request.BackText,
                RepetitionCount = 0,
                IntervalInDays = 0,
                EaseFactor = 2.5
            };
            _cardService.AddCardToDeck(deckId, card);

            return Ok(new { message = "Cartão criado!", cardId = card.Id });
        }
    }
}