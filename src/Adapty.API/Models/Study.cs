using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adapty.API.Models
{
    public class Study
    {
        public int Id { get; set; }
        public int DeckId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public StatusRevisao Status { get; set; }
    }

    public enum StatusRevisao
    {
        Pendente = 1,
        EmProgresso = 2,
        Concluido = 3,
        Ignorado = 4
    }
}