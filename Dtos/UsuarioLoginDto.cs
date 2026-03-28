using System.ComponentModel.DataAnnotations;

namespace SavingBack.Dtos
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public required string Usuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public required string Contrasena { get; set; }
    }
}
