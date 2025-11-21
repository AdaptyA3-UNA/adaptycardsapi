public class Flashcard
{
    public int Id { get; set; }
    public int DeckId { get; set; }
    public string Pergunta { get; set; } = string.Empty;
    public string Resposta { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public int NivelDominio { get; set; } = 0;

    // Propriedade de navegação para o Deck
    public Deck Deck { get; set; }
}

// Models/Deck.cs
public class Deck
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;

    // Coleção de Flashcards neste Deck
    public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
}