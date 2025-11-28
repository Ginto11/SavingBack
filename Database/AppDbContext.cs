using Microsoft.EntityFrameworkCore;
using SavingBack.Models;
using SavingBack.Utilities;
using System.Reflection.Emit;

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

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);


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
