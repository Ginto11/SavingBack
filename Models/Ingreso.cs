namespace SavingBack.Models
{
    public class Ingreso
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public required string Tipo { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public required int Monto { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
