namespace SavingBack.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        public required string PrimerNombre { get; set; } 

        public required string PrimerApellido { get; set; } 

        public long Cedula { get; set; }  

        public required string Correo { get; set; } 

        public DateTime FechaNacimiento { get; set; }

        public bool ManejaGastos { get; set; }

        public string? FotoPerfil { get; set; }

        public IFormFile? NuevaFoto { get; set; }
    }

}
