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
                        Id = a.Id,
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

        public async Task<ResultadoPagina<AhorroDto>> ObtenerTodosLosAhorrosPaginadosPorUsuarioId(int id, int paginaActual, int tamanoPagina)
        {
            try
            {
                var totalRegistros = await context.Ahorro
                    .Where(ahorro => ahorro.UsuarioId == id)
                    .CountAsync();

                var registroInicial = ((paginaActual -1 ) * tamanoPagina) + 1;
                var registroFinal = ((paginaActual - 1) * tamanoPagina) + tamanoPagina;

                var totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);

                if (paginaActual > totalPaginas)
                    throw new Exception("En la siguiente pagina no hay registros.");

                if(paginaActual == totalPaginas)
                {
                    registroFinal = (totalRegistros % tamanoPagina) + (tamanoPagina * (paginaActual -1));
                }

                var ahorros = await context.Ahorro
                    .Where(ahorro => ahorro.UsuarioId == id)
                    .Select(ahorro => new AhorroDto
                    {
                        Descripcion = ahorro.Descripcion!,
                        Id = ahorro.Id,
                        MetaAhorroNombre = ahorro.MetaAhorro!.Nombre,
                        TipoAhorro = ahorro.TipoAhorro,
                        Fecha = ahorro.Fecha,
                        Monto = ahorro.Monto,
                        EstadoMeta = ahorro.MetaAhorro.Estado!
                    })
                    .Skip((paginaActual - 1) * tamanoPagina)
                    .Take(tamanoPagina)
                    .OrderByDescending(ahorro => ahorro.Id)
                    .ToListAsync();

                return new ResultadoPagina<AhorroDto>
                {
                    TotalPaginas = totalPaginas,
                    TotalRegistros = totalRegistros,
                    TamanoPagina = tamanoPagina,
                    PaginaActual = paginaActual,
                    RegistroFinal = registroFinal,
                    RegistroInicial = registroInicial,
                    Data = ahorros
                };

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResultadoPagina<AhorroDto>> ObtenerAhorrosPorDescripcionPaginadosPorUsuarioId(int id, int paginaActual, int tamanoPagina, string nombre)
        {
            try
            {
                var totalRegistros = await context.Ahorro
                    .Where(ahorro => ahorro.UsuarioId == id && ahorro.Descripcion!.Contains(nombre))
                    .CountAsync();

                var registroInicial = ((paginaActual - 1) * tamanoPagina) + 1;
                var registroFinal = ((paginaActual - 1) * tamanoPagina) + tamanoPagina;

                var totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);

                if (paginaActual > totalPaginas)
                    throw new Exception("En la siguiente pagina no hay registros.");

                if (paginaActual == totalPaginas)
                {
                    registroFinal = (totalRegistros % tamanoPagina) + (tamanoPagina * (paginaActual - 1));
                }

                var ahorros = await context.Ahorro
                    .Where(ahorro => ahorro.UsuarioId == id && ahorro.Descripcion!.Contains(nombre))
                    .Select(ahorro => new AhorroDto
                    {
                        Descripcion = ahorro.Descripcion!,
                        Id = ahorro.Id,
                        MetaAhorroNombre = ahorro.MetaAhorro!.Nombre,
                        TipoAhorro = ahorro.TipoAhorro,
                        Fecha = ahorro.Fecha,
                        Monto = ahorro.Monto,
                        EstadoMeta = ahorro.MetaAhorro.Estado!
                    })
                    .Skip((paginaActual - 1) * tamanoPagina)
                    .Take(tamanoPagina)
                    .OrderByDescending(ahorro => ahorro.Id)
                    .ToListAsync();

                return new ResultadoPagina<AhorroDto>
                {
                    TotalPaginas = totalPaginas,
                    TotalRegistros = totalRegistros,
                    TamanoPagina = tamanoPagina,
                    PaginaActual = paginaActual,
                    RegistroFinal = registroFinal,
                    RegistroInicial = registroInicial,
                    Data = ahorros
                };

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

        public async Task Eliminar(Ahorro entidad)
        {
            try
            {
                context.Remove(entidad);
                await context.SaveChangesAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Ahorro?> BuscarPorId(int id)
        {
            try
            {
                return await context.Ahorro.FirstOrDefaultAsync(ahorro => ahorro.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
