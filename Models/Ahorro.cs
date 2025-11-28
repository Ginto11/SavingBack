using Microsoft.EntityFrameworkCore;

namespace SavingBack.Models
{
    public class Ahorro
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        [Precision(18, 2)]
        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public string? Descripcion { get; set; }

        public int? MetaAhorroId { get; set; }
        public MetaAhorro? MetaAhorro { get; set; }
    }
}
