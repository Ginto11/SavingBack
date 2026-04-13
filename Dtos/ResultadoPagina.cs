namespace SavingBack.Dtos
{

    public class ResultadoPagina<T>
    {
        public int TotalPaginas { get; set; }
        public int TotalRegistros { get; set; }
        public int PaginaActual { get; set; }
        public int TamanoPagina { get; set; }
        public int RegistroInicial { get; set; }
        public int RegistroFinal { get; set; }


        public List<T>? Data { get; set; }
    }

    public class AhorroDto
    {
        public int Id { get; set; }

        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public required string Descripcion { get; set; }

        public required string MetaAhorroNombre { get; set; }

        public required string TipoAhorro { get; set; }

        public required string EstadoMeta { get; set; }
    }

}
