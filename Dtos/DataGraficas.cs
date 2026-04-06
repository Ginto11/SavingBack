namespace SavingBack.Dtos
{
    public class DataGraficas
    {
        public List<AhorroPorDias>? ListaAhorroPorDias { get; set; }

        public List<IngresoPorDias>? ListaIngresoPorDias { get; set; }

        public List<EgresoPorDias>? ListaEgresoPorDias { get; set; }

        public List<EgresoPorCategorias>? ListaEgresoPorCategoria { get; set; }
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

