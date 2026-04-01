using Microsoft.EntityFrameworkCore;
using SavingBack.Models;
using SavingBack.Utilities;

namespace SavingBack.Database
{
    public class AppDbContext : DbContext
    {
        private readonly Utilidad utilidad;
        public AppDbContext(DbContextOptions<AppDbContext> opciones, Utilidad utilidad): base(opciones)
        {
            this.utilidad = utilidad;
        }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Ahorro> Ahorro { get; set; }

        public DbSet<MetaAhorro> MetaAhorro { get; set; }

        public DbSet<CategoriaGasto> CategoriaGasto { get; set; }

        public DbSet<Ingreso> Ingreso { get; set; }

        public DbSet<Egreso> Egreso { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<Ingreso>()
                .ToTable(tabla => tabla.HasCheckConstraint("CK_Ingreso_Tipo", "[Tipo] IN ('Efectivo', 'App', 'Nequi', 'Banco')"));

            model.Entity<Ingreso>()
                .HasData(
                    new Ingreso { Id = 1, FechaRegistro = DateTime.Now, Tipo = "Efectivo", Monto = 10000, UsuarioId = 2 },
                    new Ingreso { Id = 2, FechaRegistro = DateTime.Now, Tipo = "Nequi", Monto = 80000, UsuarioId = 2 },
                    new Ingreso { Id = 3, FechaRegistro = DateTime.Now, Tipo = "App", Monto = 120000, UsuarioId = 2 },
                    new Ingreso { Id = 4, FechaRegistro = DateTime.Now, Tipo = "Efectivo", Monto = 66000, UsuarioId = 2 },
                    new Ingreso { Id = 5, FechaRegistro = DateTime.Now, Tipo = "Banco", Monto = 150000, UsuarioId = 2 }
                );

            model.Entity<Egreso>()
                .ToTable(tabla => tabla.HasCheckConstraint("CK_Egreso_Tipo", "[Tipo] IN ('Efectivo', 'App', 'Nequi', 'Banco')"));

            model.Entity<Egreso>()
                .HasData(
                    new Egreso { Id = 1, FechaRegistro = DateTime.Now, Tipo = "Nequi", Monto = 100000, UsuarioId = 2, CategoriaGastoId = 1 },
                    new Egreso { Id = 2, FechaRegistro = DateTime.Now, Tipo = "App", Monto = 8000, UsuarioId = 2, CategoriaGastoId = 4 },
                    new Egreso { Id = 3, FechaRegistro = DateTime.Now, Tipo = "Efectivo", Monto = 12000, UsuarioId = 2, CategoriaGastoId = 10 },
                    new Egreso { Id = 4, FechaRegistro = DateTime.Now, Tipo = "Efectivo", Monto = 60000, UsuarioId = 2, CategoriaGastoId = 5 },
                    new Egreso { Id = 5, FechaRegistro = DateTime.Now, Tipo = "Banco", Monto = 180000, UsuarioId = 2, CategoriaGastoId = 2 }

                );

            model.Entity<Usuario>()
                .HasData(
                    new Usuario { 
                        Id = 1, 
                        PrimerNombre = "Admin", 
                        PrimerApellido = "", 
                        Cedula = 1001944317, 
                        NombreUsuario = "Root", 
                        Contrasena = utilidad.Encriptar("0311"), 
                        Correo = "Salinitosnelson@gmail.com", 
                        FechaNacimiento = new DateTime(2001, 08, 11), 
                        AceptaTerminos = true, Rol = "Admin" }
                );

            model.Entity<CategoriaGasto>().HasData(
                new CategoriaGasto { Id = 1, Nombre = "Alimentación" },
                new CategoriaGasto { Id = 2, Nombre = "Transporte" },
                new CategoriaGasto { Id = 3, Nombre = "Salud" },
                new CategoriaGasto { Id = 4, Nombre = "Hogar" },
                new CategoriaGasto { Id = 5, Nombre = "Servicios" },
                new CategoriaGasto { Id = 6, Nombre = "Educación" },
                new CategoriaGasto { Id = 7, Nombre = "Entretenimiento" },
                new CategoriaGasto { Id = 8, Nombre = "Mascotas" },
                new CategoriaGasto { Id = 9, Nombre = "Ropa" },
                new CategoriaGasto { Id = 10, Nombre = "Deudas" },
                new CategoriaGasto { Id = 11, Nombre = "Inversiones" },
                new CategoriaGasto { Id = 12, Nombre = "Otros" }
            );

            model.Entity<Usuario>().ToTable(t => t.HasCheckConstraint("CK_Usuario_Rol", "Rol IN ('Cliente', 'Admin')"));

            model.Entity<MetaAhorro>().ToTable(t => t.HasCheckConstraint("CK_MetaAhorro_Estado", "Estado IN ('Activa', 'Cumplida', 'Cancelada')"));

        }
    }
}
