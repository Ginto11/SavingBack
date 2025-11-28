using Microsoft.EntityFrameworkCore;
using SavingBack.Database;
using SavingBack.Dtos;
using SavingBack.Models;

namespace SavingBack.Services
{
    public class MetaAhorroService 
    {
        private readonly AppDbContext context;
        public MetaAhorroService(AppDbContext context)
        {
            this.context = context;
        }


        public async Task Insertar(MetaAhorro entidad)
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

        public async Task<IEnumerable<MetaAhorro>> BuscarMetasActivasPorUsuarioId(int id)
        {
            try
            {
                return await context.MetaAhorro
                    .Where(meta => meta.UsuarioId == id && meta.Estado == "Activa")
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        } 

        public async Task<IEnumerable<MetaAhorro>> BuscarTodasLasMetasPorUsuarioId(int id)
        {
            try
            {
                return await context.MetaAhorro
                    .Where(meta => meta.UsuarioId == id)
                    .OrderByDescending(meta => meta.Id)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CumplimientoMetaAhorro>> BuscarMetasActivasConProgresoPorUsuarioId(int id)
        {
            try
            {
                return await context.MetaAhorro
                    .Where(meta => meta.UsuarioId == id && meta.Estado == "Activa")
                    .Select(metaAhorro => new CumplimientoMetaAhorro
                    {
                        Id = metaAhorro.Id,
                        MontoActual = metaAhorro.MontoActual,
                        MontoObjetivo = metaAhorro.MontoObjetivo,
                        Porcentaje = (metaAhorro.MontoActual * 100) / metaAhorro.MontoObjetivo,
                        Nombre = metaAhorro.Nombre,
                        Estado = metaAhorro.Estado!
                    })
                    .Take(4)
                    .OrderByDescending(metaAhorro => metaAhorro.Id)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MetaAhorro?> ObtenerPorId(int id)
        {
            try
            {
                return await context.MetaAhorro
                    .FirstOrDefaultAsync(meta => meta.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Actualizar(MetaAhorro meta)
        {
            try
            {
                context.Update(meta);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
