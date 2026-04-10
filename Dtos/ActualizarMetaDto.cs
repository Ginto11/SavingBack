using System.ComponentModel.DataAnnotations;

namespace SavingBack.Dtos
{
    public class ActualizarMetaDto
    {

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "El nombre de la meta es requerido.")]
        public required string Nombre { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "El campo {0} debe ser mayor a 0.")]
        public required decimal MontoObjetivo { get; set; }
    }
}
