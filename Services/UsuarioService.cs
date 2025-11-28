using Microsoft.EntityFrameworkCore;
using SavingBack.Database;
using SavingBack.Dtos;
using SavingBack.Models;
using SavingBack.Utilities;

namespace SavingBack.Services
{
    public class UsuarioService 
    {
        private readonly AppDbContext context;
        private readonly Utilidad utilidad;
        public UsuarioService(AppDbContext context, Utilidad utilidad) 
        {
            this.utilidad = utilidad;
            this.context = context;
        }
        public async Task Actualizar(Usuario entidad)
        {
            try
            {
                context.Update(entidad);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Usuario?> BuscarPorId(int id)
        {
            try
            {
                return await context.Usuario.FirstOrDefaultAsync(usuario => usuario.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Usuario>> BuscarTodos()
        {
            try
            {
                return await context.Usuario.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Eliminar(Usuario entidad)
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

        public async Task Insertar(Usuario entidad)
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

        public async Task<Usuario?> BuscarPorContrasenaYUsuario(UsuarioLoginDto usuario)
        {
            try
            {
                var contrasenaE = utilidad.Encriptar(usuario.Contrasena);

                return await context.Usuario.FirstOrDefaultAsync(u => u.Contrasena == contrasenaE && u.NombreUsuario == usuario.Usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
