using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SavingBack.Models
{
    public class Ahorro
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        [Precision(18, 2)]
        [Range(1, 100000000, ErrorMessage = "El campo Monto debe ser mayor a 0")]
        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        [MinLength(1, ErrorMessage = "El campo Descripcion es requerido.")]
        public string? Descripcion { get; set; }

        public int? MetaAhorroId { get; set; }
        public MetaAhorro? MetaAhorro { get; set; }
    }
}
