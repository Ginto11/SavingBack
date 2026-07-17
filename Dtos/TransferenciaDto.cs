namespace SavingBack.Dtos
{
    public class TransferenciaDTO {

        public int UsuarioId { get; set; }
        public required string TipoActual { get; set; }
        public required string TipoDestino { get; set; }
        public int Monto { get; set; }
        public int CostoTransferencia { get; set; }
    }
}
