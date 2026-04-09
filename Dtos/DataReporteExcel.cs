using Microsoft.EntityFrameworkCore;
using SavingBack.Models;
using System.ComponentModel.DataAnnotations;

namespace SavingBack.Dtos
{
    public class DataReporteExcel
    {
        public List<IngresoReporteExcel>? ListaIngresosReporteExcel { get; set; }
        public List<EgresoReporteExcel>? ListaEgresosReporteExcel { get; set; }
        public List<AhorroReporteExcel>? ListaAhorrosReporteExcel { get; set; }
    }

    public class AhorroReporteExcel
    {
        public int Id { get; set; }

        public decimal Monto { get; set; }

        public DateTime FechaRegistroAhorro { get; set; }

        public required string DescripcionAhorro { get; set; }

        public int MetaId { get; set; }

        public required string NombreMeta { get; set; }

        public decimal MontoObjetivo { get; set; }

        public decimal? MontoActual { get; set; }

        public required string EstadoMeta { get; set; }

    }

    public class IngresoReporteExcel
    {
        public int Id { get; set; }

        public required string Tipo { get; set; }

        public DateTime FechaRegistro { get; set; } 

        public required int Monto { get; set; }


    }

    public class EgresoReporteExcel
    {
        public int Id { get; set; }

        public required string Tipo { get; set; }

        public DateTime FechaRegistro { get; set; }

        public required int CategoriaGastoId { get; set; }

        public required string NombreCategoria { get; set; }

        public required int Monto { get; set; }
    }
}

