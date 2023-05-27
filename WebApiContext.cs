using Microsoft.EntityFrameworkCore;

namespace blogApi.Models
{
    public partial class WebApiContext : DbContext
    {
        public WebApiContext()
        {
        }

        public WebApiContext(DbContextOptions<WebApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Poem> Poem { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configura tu cadena de conexión aquí
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=JUANDA\\SQLEXPRESS;Database=WEB_API;TrustServerCertificate=True;Trusted_Connection=True;");
                // Server=tcp:desapp-server.database.windows.net,1433;Initial Catalog=WEB_API;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication="Active Directory Default";
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser).HasName("PK__USERS__09889210DD4995A1");

                entity.ToTable("USERS");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.HasKey(e => e.IdType).HasName("PK__TYPE__09889210DD4995A1");

                entity.ToTable("TYPE");

                entity.Property(e => e.NameType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Poem>(entity =>
            {
                entity.HasKey(e => e.IdPoem).HasName("PK__POEM__09889210DD4995A1");

                entity.ToTable("POEM");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TextPoem)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IdUser)
                    .HasColumnName("IdUser")
                    .IsRequired();

                entity.Property(e => e.IdType)
                    .HasColumnName("IdType")
                    .IsRequired();
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.IdComment).HasName("PK__COMMENT__09889210DD4995A1");

                entity.ToTable("COMMENT");

                entity.Property(e => e.TxtCom)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IdUser)
                    .HasColumnName("IdUser")
                    .IsRequired();

                entity.Property(e => e.IdPoem)
                    .HasColumnName("IdPoem")
                    .IsRequired();
            });



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
