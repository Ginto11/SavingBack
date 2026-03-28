using System.ComponentModel.DataAnnotations;

namespace SavingBack.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Primer Nombre es requerido.")]
        public required string PrimerNombre { get; set; }

        [Required(ErrorMessage = "El campo Primer Apellido es requerido.")]
        public required string PrimerApellido { get; set; }

        [Required(ErrorMessage = "El campo Cédula es requerido.")]
        public long Cedula { get; set; }

        [Required(ErrorMessage = "El campo Usuario es requerido.")]
        public required string NombreUsuario { get; set; }

        [EmailAddress]
        public required string Correo { get; set; }

        [Required(ErrorMessage = "El campo Fecha de Nacimiento es requerido.")]
        [DataType(DataType.Date)]
        public required DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es requerido.")]
        [MinLength(8, ErrorMessage = "El campo Contraseña debe tener mas  de 8 caracteres.")]
        public required string Contrasena { get; set; }

        [Required(ErrorMessage = "El campo Acepta Terminos es requerido.")]
        public required bool AceptaTerminos { get; set; }

        public bool ManejaGastos { get; set; } = false;

        public string? Rol { get; set; }

        public string FotoPerfil { get; set;  } = "/Uploads/Fotos/default.png";
    }
}
