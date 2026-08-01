using Microsoft.EntityFrameworkCore;
using SavingBack.Database;
using SavingBack.Dtos;

namespace SavingBack.Services
{
    public class PdfService
    {
        private readonly AppDbContext context;

        public PdfService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<DataPdfMetaAhorro> ObtenerDataPdfMetaAhorro(int metaAhorroId)
        {
            try
            {
                var meta = await ObtenerMetaPdf(metaAhorroId);
                var usuario = await ObtenerUsuarioPdf(metaAhorroId);
                var resumen = await ObtenerResumenPdf(metaAhorroId);
                var ingresos = await ObtenerIngresosPdf(metaAhorroId);
                var detalles = await ObtenerDetallesAdicionalesPdf(metaAhorroId);

                return new DataPdfMetaAhorro
                {
                    DetalleAdicional = detalles,
                    ListaIngresos = ingresos,
                    Meta = meta,
                    Resumen = resumen,
                    Usuario = usuario
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UsuarioPdf> ObtenerUsuarioPdf(int metaAhorroId)
        {
            try
            {
                var meta = await context.MetaAhorro
                    .Include(meta => meta.Usuario)
                    .FirstOrDefaultAsync(meta => meta.Id == metaAhorroId);

                return new UsuarioPdf
                {
                    NombreUsuario = meta!.Usuario!.PrimerNombre + " " + meta.Usuario.PrimerApellido,
                    CorreoUsuario = meta.Usuario.Correo
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MetaPdf> ObtenerMetaPdf(int metaAhorroId)
        {
            try
            {
                var meta = await context.MetaAhorro.FirstOrDefaultAsync(meta => meta.Id == metaAhorroId);

                return new MetaPdf
                {
                    EstadoMeta = meta!.Estado,
                    FechaCreacion = meta.FechaCreacion,
                    FechaCumplimiento = meta.FechaCumplimiento,
                    MontoActual = meta.MontoActual,
                    MontoObjetivo = meta.MontoObjetivo,
                    NombreMeta = meta.Nombre
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<IngresoPdf>> ObtenerIngresosPdf(int metaAhorroId)
        {
            try
            {
                return await context.Ahorro
                    .Where(ahorro => ahorro.MetaAhorroId == metaAhorroId)
                    .Select(ahorro => new IngresoPdf
                    {
                        Descripcion = ahorro.Descripcion,
                        Fecha = ahorro.Fecha,
                        Id = ahorro.Id,
                        Monto = ahorro.Monto,
                        TipoAhorro = ahorro.TipoAhorro
                    })
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResumenPdf> ObtenerResumenPdf(int metaAhorroId)
        {
            try
            {
                var totalIngresos = await context.Ahorro
                    .Where(ahorro => ahorro.MetaAhorroId == metaAhorroId)
                    .Select(ahorro => (decimal?) ahorro.Monto)
                    .SumAsync() ?? 0;

                var promedio = await context.Ahorro
                    .Where(ahorro => ahorro.MetaAhorroId == metaAhorroId)
                    .Select(ahorro => (decimal?) ahorro.Monto)
                    .AverageAsync() ?? 0;

                var cantidadMovimientos = context.Ahorro
                    .Where(ahorro => ahorro.MetaAhorroId == metaAhorroId)
                    .Count();

                var montoActual = await context.MetaAhorro
                    .Where(meta => meta.Id == metaAhorroId)
                    .Select(meta => (int) meta.MontoActual!)
                    .FirstOrDefaultAsync();

                var montoObjetivo = await context.MetaAhorro
                    .Where(meta => meta.Id == metaAhorroId)
                    .Select(meta => (int) meta.MontoObjetivo)
                    .FirstOrDefaultAsync();

                var calculo = (montoActual * 100) / montoObjetivo;

                return new ResumenPdf
                {
                    CantidadMovimientos = cantidadMovimientos,
                    Promedio = promedio,
                    TotalIngresos = totalIngresos,
                    Porcentaje = calculo
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DetalleAdicionalPdf> ObtenerDetallesAdicionalesPdf(int metaAhorroId)
        {
            try
            {
                var mayorIngreso = await context.Ahorro
                    .Where(ahorro => ahorro.MetaAhorroId == metaAhorroId)
                    .Select(ahorro => (decimal?) ahorro.Monto)
                    .MaxAsync() ?? 0;

                var menorIngreso = await context.Ahorro
                    .Where(ahorro => ahorro.MetaAhorroId == metaAhorroId)
                    .Select(ahorro => (decimal?) ahorro.Monto)
                    .MinAsync() ?? 0;

                var fechaUltimoMovimiento = await context.Ahorro
                    .Where(a => a.MetaAhorroId == metaAhorroId)
                    .OrderByDescending(a => a.Fecha)
                    .Select(a => a.Fecha)
                    .FirstOrDefaultAsync();

                return new DetalleAdicionalPdf
                {
                    FechaUltimoMovimiento = fechaUltimoMovimiento,
                    MayorIngreso = mayorIngreso,
                    MenorIngreso = menorIngreso
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
