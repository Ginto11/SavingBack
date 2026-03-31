namespace SavingBack.Dtos
{
    public class UltimoMovimientoAhorro
    {
        public required int Id { get; set; }

        public required string Descripcion { get; set; }
        
        public DateTime FechaAhorro { get; set; }

        public decimal Monto { get; set; }
    }
}
