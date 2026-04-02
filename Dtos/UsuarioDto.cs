using System.ComponentModel.DataAnnotations;

namespace SavingBack.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Primer Nombre es requerido.")]
        public required string PrimerNombre { get; set; }

        [Required(ErrorMessage = "El campo Primer Apellido es requerido.")]
        public required string PrimerApellido { get; set; } 

        public long Cedula { get; set; }

        [Required(ErrorMessage = "El campo Correo es requerido.")]
        public required string Correo { get; set; } 

        public DateTime FechaNacimiento { get; set; }

        public bool ManejaGastos { get; set; }

        public string? FotoPerfil { get; set; }

        public IFormFile? NuevaFoto { get; set; }
    }

}
