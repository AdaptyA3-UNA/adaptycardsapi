using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adapty.API.Models
{
    public class Cards
    {
        public int Id { get; set; }
    public int DeckId { get; set; }
    public string Pergunta { get; set; } = string.Empty;
    public string Resposta { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }

    public Study study { get; set; } = new Study();
    
    public NivelDominio nivel { get; set; } = new NivelDominio(); // 0 = Novo, 1 = Reiniciar, 2 = Revisar, 3 = Já sei, 4 = Embaralhar

    // Propriedade de navegação para o Deck
    public Decks decks { get; set; } = new Decks();

    public enum NivelDominio
    {
        Reiniciar = 1,
        Revisar = 2,
        jaSei = 3,
        Embaralhar = 4
    }
    }
}