namespace SavingBack.Dtos
{
    public class CrearAhorroDto
    {
        
        public int UsuarioId { get; set; }
        
        public decimal Monto { get; set; }

        public string? Descripcion { get; set; }

        public int MetaAhorroId { get; set; }
    }
}
