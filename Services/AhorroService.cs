using Microsoft.EntityFrameworkCore;
using SavingBack.Database;
using SavingBack.Dtos;
using SavingBack.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SavingBack.Services
{
    public class AhorroService
    {
        private readonly AppDbContext context;

        public AhorroService(AppDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Ahorro>> BuscarTodos()
        {
            try
            {
                return await context.Ahorro.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UltimoMovimientoAhorro>> ObtenerUltimosMovimientosPorUsuarioId(int id)
        {
            try
            {
                return await context.Ahorro
                    .Where(a => a.UsuarioId == id)
                    .OrderByDescending(a => a.Fecha)
                    .Select(a => new UltimoMovimientoAhorro
                    {
                        Descripcion = a.Descripcion!,
                        FechaAhorro = a.Fecha,
                        Monto = a.Monto
                    })
                    .Take(4)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CantidadesAhorro> ObtenerCantidadesTotalesPorUsuarioId(int id)
        {
            try
            {
                var totalAhorro = await context.MetaAhorro
                    .Where(ahorro => ahorro.UsuarioId == id && ahorro.Estado == "Activa")
                    .SumAsync(ahorro => ahorro.MontoActual);

                var hoy = DateTime.Now;

                var totalMes = await context.Ahorro
                    .Where(a => a.UsuarioId == id &&
                                a.Fecha.Year == hoy.Year &&
                                a.Fecha.Month == hoy.Month)
                    .SumAsync(a => a.Monto);

                return new CantidadesAhorro
                {
                    TotalAhorrado = totalAhorro,
                    AhorroMes = totalMes
                };

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Ahorro>> ObtenerTodosPorUsuarioId(int id)
        {
            try
            {
                return await context.Ahorro
                    .Where(ahorro => ahorro.UsuarioId == id)
                    .ToListAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task Insertar(Ahorro entidad)
        {
            try
            {
                context.Add(entidad);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
