namespace SavingBack.Dtos
{
    public class UsuarioLogueadoDto
    {
        public int Id { get; set; }

        public required string PrimerNombre { get; set; }
        
        public required string Token { get; set; }
        
        public required string Rol { get; set; }
    }
}
