using Microsoft.EntityFrameworkCore;
using SavingBack.Database;
using SavingBack.Dtos;

namespace SavingBack.Services
{
    public class ReporteService
    {

        private readonly AppDbContext appDbContext;

        public ReporteService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<DataReporteExcel> ObtenerDatos(int id)
        {
            var listaIngresosReporteExcel = await ObtenerListaIngresosReporteExcel(id);
            var listaEgresosReporteExcel = await ObtenerListaEgresosReporteExcel(id);
            var listaAhorrosReporteExcel = await ObtenerListaAhorrosReporteExcel(id);

            return new DataReporteExcel
            {
                ListaIngresosReporteExcel = listaIngresosReporteExcel,
                ListaEgresosReporteExcel = listaEgresosReporteExcel,
                ListaAhorrosReporteExcel = listaAhorrosReporteExcel
            };
        }

        public async Task<List<IngresoReporteExcel>> ObtenerListaIngresosReporteExcel(int id)
        {
            return await appDbContext.Ingreso
                .Where(ingreso => ingreso.UsuarioId == id)
                .Select(ingreso => new IngresoReporteExcel
                {
                    Id = ingreso.Id,
                    FechaRegistro = ingreso.FechaRegistro,
                    Tipo = ingreso.Tipo,
                    Monto = ingreso.Monto
                })
                .ToListAsync();
        }

        public async Task<List<EgresoReporteExcel>> ObtenerListaEgresosReporteExcel(int id)
        {
            return await appDbContext.Egreso
                .Where(egreso => egreso.UsuarioId == id)
                .Select(egreso => new EgresoReporteExcel
                {
                    Id = egreso.Id,
                    FechaRegistro = egreso.FechaRegistro,
                    Tipo = egreso.Tipo,
                    Monto = egreso.Monto,
                    CategoriaGastoId = egreso.CategoriaGastoId,
                    NombreCategoria = egreso.CategoriaGasto!.Nombre
                })
                .ToListAsync();
        }

        public async Task<List<AhorroReporteExcel>> ObtenerListaAhorrosReporteExcel(int id)
        {
            return await appDbContext.Ahorro
                .Where(ahorro => ahorro.UsuarioId == id)
                .Select(ahorro => new AhorroReporteExcel
                {
                    Id = ahorro.Id,
                    Monto = ahorro.Monto,
                    FechaRegistroAhorro = ahorro.Fecha,
                    DescripcionAhorro = ahorro.Descripcion!,
                    MetaId = ahorro.MetaAhorro!.Id,
                    NombreMeta = ahorro.MetaAhorro!.Nombre,
                    MontoActual = ahorro.MetaAhorro.MontoActual,
                    MontoObjetivo = ahorro.MetaAhorro.MontoObjetivo,
                    EstadoMeta = ahorro.MetaAhorro.Estado!
                })
                .ToListAsync();
        }
    }
}
