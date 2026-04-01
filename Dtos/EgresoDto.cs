using System.ComponentModel.DataAnnotations;

namespace SavingBack.Dtos
{
    public class EgresoDto
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        [MinLength(3, ErrorMessage = "El campo {0} es requerido")]
        public required string Tipo { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} tiene que ser mayor a 0")]
        public int Monto { get; set; }

        public int CategoriaGastoId { get; set; }
    }
}
