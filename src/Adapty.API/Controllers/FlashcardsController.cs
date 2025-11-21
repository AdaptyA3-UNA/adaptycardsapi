using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Adapty.API.Models;
namespace Adapty.API.Data;


[Route("api/[controller]")]
[ApiController]
public class FlashcardsController : ControllerBase
{
    private readonly FlashcardContext _context;

    public FlashcardsController(FlashcardContext context)
    {
        _context = context;
    }

    // GET: api/Flashcards
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Flashcard>>> GetFlashcards()
    {
        // Retorna a lista de todos os Flashcards
        return await _context.Flashcards.ToListAsync();
    }

    // POST: api/Flashcards
    [HttpPost]
    public async Task<ActionResult<Flashcard>> PostFlashcard(Flashcard flashcard)
    {
        flashcard.DataCriacao = DateTime.UtcNow;
        _context.Flashcards.Add(flashcard);
        await _context.SaveChangesAsync();

        // Retorna o código 201 Created e o objeto criado
        return CreatedAtAction(nameof(GetFlashcards), new { id = flashcard.Id }, flashcard);
    }

    // ... outros métodos (GET por ID, PUT, DELETE)
}