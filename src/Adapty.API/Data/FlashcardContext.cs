using Microsoft.EntityFrameworkCore;
namespace Adapty.API.Models;

public class FlashcardContext : DbContext
{
    public FlashcardContext(DbContextOptions<FlashcardContext> options)
        : base(options)
    {
    }

    public DbSet<Flashcard> Flashcards { get; set; }
    public DbSet<Deck> Decks { get; set; }
}