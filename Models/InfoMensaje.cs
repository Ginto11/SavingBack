namespace SavingBack.Models
{
    public class InfoMensaje
    {
        public required string NombreUsuariAhorro { get; set; }

        public required string DescripcionAhorro { get; set; }

        public required string NombreMetaAhorro { get; set; }

        public DateTime FechaAhorro { get; set; } = DateTime.Now;

        public required decimal MontoAhorro { get; set; }

    }
}
