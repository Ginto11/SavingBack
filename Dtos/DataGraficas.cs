namespace SavingBack.Dtos
{
    public class DataGraficas
    {
        public List<AhorroPorDias>? ListaAhorroPorDias { get; set; }

        public List<IngresoPorDias>? ListaIngresoPorDias { get; set; }

        public List<EgresoPorDias>? ListaEgresoPorDias { get; set; }

        public List<EgresoPorCategorias>? ListaEgresoPorCategoria { get; set; }

        public List<MetaCumplimientoGrafica>? ListaMetaCumplimiento { get; set; }

        public List<Rentabilidad>? ListaRentabilidad { get; set; }
    }

    public class Rentabilidad
    {
        public int Dia { get; set; }

        public decimal Diferencia { get; set; }
    }

    public class MetaCumplimientoGrafica
    {
        public required string NombreMeta { get; set; }

        public decimal? MontoActual { get; set; }

        public decimal MontoObjetivo { get; set; }
    }

    public class EgresoPorCategorias
    {
        public required string NombreCategoria { get; set; }
        public decimal Total { get; set; }
    }

    public class AhorroPorDias
    {
        public int Dia { get; set; }
        public decimal Total { get; set; }
    }

    public class IngresoPorDias
    {
        public int Dia { get; set; }

        public decimal Total { get; set; }
    }

    public class EgresoPorDias
    {
        public int Dia { get; set; }
        public decimal Total { get; set; }
    }
}

