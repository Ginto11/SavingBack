using Microsoft.EntityFrameworkCore;

namespace SavingBack.Models
{
    public class MetaAhorro
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public required string Nombre { get; set; }

        [Precision(18, 2)]
        public required decimal MontoObjetivo { get; set; }

        [Precision(18, 2)]
        public decimal? MontoActual { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public List<Ahorro>? Ahorros { get; set; }

        public string? Estado { get; set; }
    }
}
