using Microsoft.EntityFrameworkCore;
using SavingBack.Database;
using SavingBack.Dtos;
using SavingBack.Models;

namespace SavingBack.Services
{
    public class IngresoService 
    {
        private readonly AppDbContext context;

        public IngresoService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Insertar(IngresoDto ingreso)
        {
            try
            {
                var nuevoIngreso = new Ingreso
                {
                    UsuarioId =ingreso.UsuarioId,
                    Monto = ingreso.Monto,
                    Tipo = ingreso.Tipo,
                };

                context.Add(nuevoIngreso);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TiposIngresosTotales> ObtenerTiposTotalesIngresos(int id)
        {
            try
            {
                var totalesIngresos = await context.Ingreso
                    .Where(ingreso => ingreso.UsuarioId == id)
                    .GroupBy(ingreso => ingreso.Tipo)
                    .ToDictionaryAsync(ingreso => ingreso.Key, ingreso => ingreso.Sum(x => x.Monto));

                var totalesEgresos = await context.Egreso
                    .Where(egreso => egreso.UsuarioId == id)
                    .GroupBy(egreso => egreso.Tipo)
                    .ToDictionaryAsync(egreso => egreso.Key, ingreso => ingreso.Sum(x => x.Monto));

                var resultado = new TiposIngresosTotales(
                    totalesIngresos.GetValueOrDefault("Efectivo") - totalesEgresos.GetValueOrDefault("Efectivo"),
                    totalesIngresos.GetValueOrDefault("Nequi") - totalesEgresos.GetValueOrDefault("Nequi"),
                    totalesIngresos.GetValueOrDefault("App") - totalesEgresos.GetValueOrDefault("App")
                );

                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<IEnumerable<IngresoDto>> ListaDeIngresos(int id)
        {
            try
            {
                return await context.Ingreso
                    .Select(ingreso => new IngresoDto
                    {
                        Id = ingreso.Id,
                        UsuarioId = ingreso.UsuarioId,
                        FechaRegistro = ingreso.FechaRegistro,
                        Monto = ingreso.Monto,
                        Tipo = ingreso.Tipo
                    })
                    .Where(ingreso => ingreso.UsuarioId == id)
                    .Take(5)
                    .OrderByDescending(ingreso => ingreso.Id)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> BuscarTotalIngresoEnTipo(string tipo, int id)
        {
            try
            {
                return await context.Ingreso
                    .Where(ingreso => ingreso.Tipo == tipo && ingreso.UsuarioId == id)
                    .SumAsync(ingreso => ingreso.Monto);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
