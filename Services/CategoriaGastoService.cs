using Microsoft.EntityFrameworkCore;
using SavingBack.Database;
using SavingBack.Models;

namespace SavingBack.Services
{
    public class CategoriaGastoService
    {
        private readonly AppDbContext context;
        public CategoriaGastoService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<CategoriaGasto>> ObtenerCategoriasGastos()
        {
            try
            {
                return await context.CategoriaGasto.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
