using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TEST.Modelos;
using TEST.Modelos.DTOs;

namespace TEST.Identity
{
    public partial class MyIdentityDbContext : IdentityDbContext<Usuario, Rol, String >
    {

        public MyIdentityDbContext(DbContextOptions<MyIdentityDbContext> options)
            : base(options) 
        { 
        
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        public virtual DbSet<Habitacion> Habitaciones { get; set; }

        public virtual DbSet<Reserva> Reservas { get; set; }

        public virtual DbSet<Servicio> Servicios { get; set; }

        public virtual DbSet<ServicioUsuario> ServicioUsuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.ProviderKey });
            });

            // Si también usas IdentityUserRole o cualquier otra entidad de Identity, puedes configurarlas igualmente
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name }); // Definimos una clave primaria compuesta
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");


                entity.Property(e => e.Nif).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(256);
                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Habitacion>(entity =>
            {
                entity.HasKey(e => e.Numero);

                entity.ToTable("Habitacion");

                entity.Property(e => e.Numero).ValueGeneratedNever();
                entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.IdReserva);

                entity.ToTable("Reserva");

                entity.Property(e => e.IdReserva)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Id_Reserva");
                entity.Property(e => e.FechaEntrada)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_entrada");
                entity.Property(e => e.FechaReserva)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_reserva");
                entity.Property(e => e.FechaSalida)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_salida");
                entity.Property(e => e.IdUsuario)
                    .HasMaxLength(450)
                    .HasColumnName("Id_Usuario");
                entity.Property(e => e.NumeroH).HasColumnName("Numero_H");
                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reserva_Usuario");

                entity.HasOne(d => d.NumeroHNavigation).WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.NumeroH)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reserva_Habitacion");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.Id_Servicio);

                entity.ToTable("Servicio");

                entity.Property(e => e.Id_Servicio)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Id_Servicio");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ServicioUsuario>(entity =>
            {
                entity.ToTable("ServicioUsuario");

                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Fecha).HasColumnType("datetime");
                entity.Property(e => e.Id_Servicio).HasColumnName("Id_Servicio");
                entity.Property(e => e.Id_Usuario)
                    .HasMaxLength(450)
                    .HasColumnName("Id_Usuario");

                entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.ServicioUsuarios)
                    .HasForeignKey(d => d.Id_Servicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServicioUsuario_Servicio");

                entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.ServicioUsuarios)
                    .HasForeignKey(d => d.Id_Usuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServicioUsuario_Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
