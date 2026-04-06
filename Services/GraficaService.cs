using Microsoft.EntityFrameworkCore;
using SavingBack.Database;
using SavingBack.Dtos;
using SavingBack.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SavingBack.Services
{
    public class GraficaService
    {
        private readonly AppDbContext appDbContext;
        private readonly int anioActual;
        private readonly int mesActual;
        private readonly int diaActual;

        public GraficaService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            this.anioActual = DateTime.Now.Year;
            this.mesActual = DateTime.Now.Month;
            this.diaActual = DateTime.Now.Day;
        }

        public async Task<DataGraficas> ObtenerAhorroCompletoPorDia(int usuarioId)
        {

            var ahorros = await ObtenerListaAhorroPorDias(usuarioId);
            var ingresos = await ObtenerListaIngresoPorDias(usuarioId);
            var egresos = await ObtenerListaEgresoPorDias(usuarioId);

            return new DataGraficas { 
                ListaAhorroPorDias = ahorros, 
                ListaIngresoPorDias = ingresos, 
                ListaEgresoPorDias = egresos 
            };
        }

        public async Task<List<AhorroPorDias>> ObtenerListaAhorroPorDias(int id)
        {
            var datos = await appDbContext.Ahorro
                .Where(a => a.Fecha.Year == this.anioActual && a.Fecha.Month == this.mesActual && a.UsuarioId == id)
                .GroupBy(a => a.Fecha.Day)
                .Select(g => new AhorroPorDias
                {
                    Dia = g.Key,
                    Total = g.Sum(x => x.Monto)
                })
                .ToListAsync();

            int diasEnMes = DateTime.DaysInMonth(anioActual, mesActual);

            return Enumerable.Range(1, this.diaActual)
                .Select(dia => new AhorroPorDias
                {
                    Dia = dia,
                    Total = datos.FirstOrDefault(x => x.Dia == dia)?.Total ?? 0
                })
                .ToList();
        }

        public async Task<List<IngresoPorDias>> ObtenerListaIngresoPorDias(int id)
        {
            var datos =await  appDbContext.Ingreso
                .Where(i => i.UsuarioId == id && i.FechaRegistro.Month == this.mesActual && i.FechaRegistro.Year == this.anioActual)
                .GroupBy(i => i.FechaRegistro.Day)
                .Select(i => new IngresoPorDias
                {
                    Dia = i.Key,
                    Total = i.Sum(x => x.Monto)
                })
                .ToListAsync();

            int diasEnMes = DateTime.DaysInMonth(anioActual, mesActual);

            return Enumerable.Range(1, this.diaActual)
                .Select(dia => new IngresoPorDias
                {
                    Dia = dia,
                    Total = datos.FirstOrDefault(x => x.Dia == dia)?.Total ?? 0
                })
                .ToList();
        }

        public async Task<List<EgresoPorDias>> ObtenerListaEgresoPorDias(int id)
        {
            var datos = await appDbContext.Egreso
                .Where(i => i.UsuarioId == id && i.FechaRegistro.Month == this.mesActual && i.FechaRegistro.Year == this.anioActual)
                .GroupBy(i => i.FechaRegistro.Day)
                .Select(i => new EgresoPorDias
                {
                    Dia = i.Key,
                    Total = i.Sum(x => x.Monto)
                })
                .ToListAsync();

            int diasEnMes = DateTime.DaysInMonth(anioActual, mesActual);

            return Enumerable.Range(1, this.diaActual)
                .Select(dia => new EgresoPorDias
                {
                    Dia = dia,
                    Total = datos.FirstOrDefault(x => x.Dia == dia)?.Total ?? 0
                })
                .ToList();
        }
    }
}
