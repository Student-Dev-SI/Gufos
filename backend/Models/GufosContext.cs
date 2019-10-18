using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace backend.Models
{
    public partial class GufosContext : DbContext
    {
        public GufosContext()
        {
        }

        public GufosContext(DbContextOptions<GufosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<Localizacao> Localizacao { get; set; }
        public virtual DbSet<Presenca> Presenca { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-D4CE51M\\SQLEXPRESS; Database=Gufos; User Id=sa; Password=132");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Idcategoria)
                    .HasName("PK__categori__70E82E28B6BCD66D");

                entity.HasIndex(e => e.Titulo)
                    .HasName("UQ__categori__7B406B563B404FAB")
                    .IsUnique();

                entity.Property(e => e.Titulo).IsUnicode(false);
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.Idevento)
                    .HasName("PK__evento__E63053027212F0AA");

                entity.HasIndex(e => e.AcessoLivre)
                    .HasName("UQ__evento__52D38412E514F2DE")
                    .IsUnique();

                entity.Property(e => e.AcessoLivre).HasDefaultValueSql("((1))");

                entity.Property(e => e.Titulo).IsUnicode(false);

                entity.HasOne(d => d.IdcategoriaNavigation)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.Idcategoria)
                    .HasConstraintName("FK__evento__IDCatego__5070F446");

                entity.HasOne(d => d.IdlocalizacaoNavigation)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.Idlocalizacao)
                    .HasConstraintName("FK__evento__IDLocali__5165187F");
            });

            modelBuilder.Entity<Localizacao>(entity =>
            {
                entity.HasKey(e => e.Idlocalizacao)
                    .HasName("PK__localiza__1B3D6255D591F679");

                entity.HasIndex(e => e.Endereco)
                    .HasName("UQ__localiza__4DF3E1FF59986357")
                    .IsUnique();

                entity.HasIndex(e => e.RazãoSocial)
                    .HasName("UQ__localiza__D4D35CE991FC0F21")
                    .IsUnique();

                entity.Property(e => e.Cnpj)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Endereco).IsUnicode(false);

                entity.Property(e => e.RazãoSocial).IsUnicode(false);
            });

            modelBuilder.Entity<Presenca>(entity =>
            {
                entity.HasKey(e => e.Idpresenca)
                    .HasName("PK__presenca__FF9F967FBC3322F8");

                entity.HasIndex(e => e.PresencaStatus)
                    .HasName("UQ__presenca__AC534431E81929D8")
                    .IsUnique();

                entity.Property(e => e.PresencaStatus).IsUnicode(false);

                entity.HasOne(d => d.IdeventoNavigation)
                    .WithMany(p => p.Presenca)
                    .HasForeignKey(d => d.Idevento)
                    .HasConstraintName("FK__presenca__IDEven__5AEE82B9");

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.Presenca)
                    .HasForeignKey(d => d.Idusuario)
                    .HasConstraintName("FK__presenca__IDUsua__59FA5E80");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdtipoUsuario)
                    .HasName("PK__tipoUsua__532897544EA26001");

                entity.HasIndex(e => e.Titulo)
                    .HasName("UQ__tipoUsua__7B406B56098F79C0")
                    .IsUnique();

                entity.Property(e => e.Titulo).IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("PK__usuario__5231116901BF5F7F");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__usuario__A9D1053412941CA9")
                    .IsUnique();

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.Senha).IsUnicode(false);

                entity.HasOne(d => d.IdtipoUsuarioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdtipoUsuario)
                    .HasConstraintName("FK__usuario__IDTipoU__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
