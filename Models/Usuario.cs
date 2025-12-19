namespace SavingBack.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public required string PrimerNombre { get; set; }

        public required string PrimerApellido { get; set; }

        public long Cedula { get; set; } 

        public required string NombreUsuario { get; set; }

        public required string Correo { get; set; }

        public required DateTime FechaNacimiento { get; set; }

        public required string Contrasena { get; set; }
            
        public required bool AceptaTerminos { get; set; }
        public bool ManejaGastos { get; set; } = false;
        public string? Rol { get; set; }

        public string FotoPerfil { get; set;  } = "/Uploads/Fotos/default.png";
    }
}
