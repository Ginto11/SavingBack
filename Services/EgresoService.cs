using Microsoft.EntityFrameworkCore;
using SavingBack.Database;
using SavingBack.Dtos;
using SavingBack.Models;

namespace SavingBack.Services
{
    public class EgresoService
    {
        private readonly AppDbContext context;

        public EgresoService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Insertar(EgresoDto egreso)
        {
            try
            {
                var nuevoEgreso = new Egreso
                {
                    UsuarioId = egreso.UsuarioId,
                    Monto = egreso.Monto,
                    Tipo = egreso.Tipo,
                    CategoriaGastoId = egreso.CategoriaGastoId
                };

                context.Add(nuevoEgreso);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TiposEgresosTotales> ObtenerTiposTotalesEgresos(int id)
        {
            try
            {
                var totales = await context.Egreso
                    .Where(egreso => egreso.UsuarioId == id)
                    .GroupBy(egreso => egreso.Tipo)
                    .ToDictionaryAsync(egreso => egreso.Key, egreso => egreso.Sum(x => x.Monto));

                var resultado = new TiposEgresosTotales(
                    totales.GetValueOrDefault("Efectivo"),
                    totales.GetValueOrDefault("Nequi"),
                    totales.GetValueOrDefault("App"),
                    totales.GetValueOrDefault("Banco")
                );

                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<EgresoDto>> ListaDeEgresos(int id)
        {
            try
            {
                return await context.Egreso
                    .Select(egreso => new EgresoDto
                    {
                        Id = egreso.Id,
                        UsuarioId = egreso.UsuarioId,
                        FechaRegistro = egreso.FechaRegistro,
                        Monto = egreso.Monto,
                        Tipo = egreso.Tipo,
                        CategoriaGastoId = egreso.CategoriaGastoId
                    })
                    .Where(egreso => egreso.UsuarioId == id)
                    .Take(5)
                    .OrderByDescending(egreso => egreso.Id)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> BuscarTotalEgresoEnTipo(string tipo, int id)
        {
            try
            {
                return await context.Egreso
                    .Where(egreso => egreso.Tipo == tipo && egreso.UsuarioId == id)
                    .SumAsync(egreso => egreso.Monto);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
