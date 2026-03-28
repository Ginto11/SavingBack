using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SavingBack.Models
{
    public class MetaAhorro
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        [MinLength(1, ErrorMessage = "El campo Nombre es requerido")]
        public required string Nombre { get; set; }

        [Precision(18, 2)]
        [Range(1, 10000000, ErrorMessage = "El campo Monto Objetivo debe ser mayor a 0")]
        public decimal MontoObjetivo { get; set; }

        [Precision(18, 2)]
        public decimal? MontoActual { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public List<Ahorro>? Ahorros { get; set; }

        public string? Estado { get; set; }
    }
}
