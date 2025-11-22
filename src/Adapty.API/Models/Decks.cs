using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adapty.API.Models
{
    public class Decks
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
}