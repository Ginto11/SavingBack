namespace SavingBack.Dtos
{
    public class CumplimientoMetaAhorro
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }

        public decimal? MontoActual { get; set; }

        public required decimal MontoObjetivo { get; set; }

        public decimal? Porcentaje { get; set; }

        public required string Estado { get; set; }
    }
}
