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

        #region Endpoints

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
                EaseFactor = 2.5,
                // Define a próxima revisão como agora (disponível imediatamente)
                NextReviewDate = DateTime.Now
            };
            _cardService.AddCardToDeck(deckId, card);

            return Ok(new { message = "Cartão criado!", cardId = card.Id });
        }

        // 5. EXCLUIR UM DECK
        [HttpDelete("{id}")]
        public IActionResult DeleteDeck(int id)
        {
            var deck = _deckService.GetDeckById(id);
            if (deck == null)
            {
                return NotFound($"Deck com ID {id} não encontrado.");
            }

            try
            {
                _deckService.DeleteDeck(deck);
                return Ok(new { message = $"Deck '{deck.Nome}' excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar excluir o deck.");
            }
        }

        
        // 6. EDITAR UM DECK (PUT)
        [HttpPut("{id}")]
        public IActionResult EditDeck(int id, [FromBody] CreateDeckDto request)
        {
            var deck = _deckService.GetDeckById(id);
            if (deck == null) return NotFound($"Deck com ID {id} não encontrado.");

            deck.Nome = request.Title;
            deck.Descricao = request.Description;
            // Tags handling depende do modelo; se tiver campo Tags, atualize aqui.

            try
            {
                _deckService.UpdateDeck(deck);
                return Ok(new { message = "Deck atualizado com sucesso.", deckId = deck.Id });
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao atualizar o deck.");
            }
        }

        // 7. EDITAR UM CARTÃO (PUT)
        [HttpPut("{deckId}/cards/{cardId}")]
        public IActionResult EditCard(int deckId, int cardId, [FromBody] CreateCardDto request)
        {
            var card = _cardService.GetCardById(cardId);
            if (card == null || card.DeckId != deckId) return NotFound("Cartão não encontrado no deck informado.");

            card.Pergunta = request.FrontText;
            card.Resposta = request.BackText;
            // Opcional: manter campos de repetição / nextReview

            try
            {
                _cardService.UpdateCard(card);
                return Ok(new { message = "Cartão atualizado com sucesso.", cardId = card.Id });
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao atualizar o cartão.");
            }
        }

        // 8. EXCLUIR UM CARTÃO (DELETE)
        [HttpDelete("{deckId}/cards/{cardId}")]
        public IActionResult DeleteCard(int deckId, int cardId)
        {
            var card = _cardService.GetCardById(cardId);
            if (card == null || card.DeckId != deckId) return NotFound("Cartão não encontrado no deck informado.");

            try
            {
                _cardService.DeleteCard(card);
                return Ok(new { message = "Cartão excluído com sucesso." });
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao excluir o cartão.");
            }
        }
        #endregion
    }
}
