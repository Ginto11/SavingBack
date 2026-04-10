using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SavingBack.Dtos
{
    public class CrearAhorroDto
    {
        
        public int UsuarioId { get; set; }

        [Precision(18, 2)]
        [Range(1, 100000000, ErrorMessage = "El campo Monto debe ser mayor a 0")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public decimal Monto { get; set; }

        [MinLength(1, ErrorMessage = "El campo Descripcion es requerido.")]
        public required string Descripcion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El campo Meta es requerido.")]
        public int MetaAhorroId { get; set; }

        [Required(ErrorMessage = "El campo Tipo de ahorro es requerido.")]
        public required string TipoAhorro { get; set; }
    }
}
