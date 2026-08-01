namespace SavingBack.Dtos
{
    public class DataPdfMetaAhorro
    {
        public UsuarioPdf? Usuario { get; set; }

        public MetaPdf? Meta { get; set; }

        public List<IngresoPdf>? ListaIngresos { get; set; }

        public ResumenPdf? Resumen { get; set; }

        public DetalleAdicionalPdf? DetalleAdicional { get; set; }

    }

    public class UsuarioPdf
    {
        public string? NombreUsuario { get; set; }
        public string? CorreoUsuario { get; set; }
    }

    public class MetaPdf
    {
        public string? NombreMeta { get; set; }

        public string? EstadoMeta { get; set; }

        public decimal MontoObjetivo { get; set; }

        public decimal? MontoActual { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaCumplimiento { get; set; }
    }

    public class IngresoPdf
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public string? Descripcion { get; set; }

        public string? TipoAhorro { get; set; }

        public decimal Monto { get; set; }
    }

    public class ResumenPdf
    {
        public decimal TotalIngresos { get; set; }

        public decimal Promedio { get; set; }

        public int CantidadMovimientos { get; set; }

        public int Porcentaje { get; set; }

    }

    public class DetalleAdicionalPdf
    {
        public decimal MayorIngreso { get; set; }

        public decimal MenorIngreso { get; set; }

        public DateTime FechaUltimoMovimiento { get; set; }
    }
}
