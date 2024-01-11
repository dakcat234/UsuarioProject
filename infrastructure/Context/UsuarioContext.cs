using Business.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace infrastructure.Context
{
    public class UsuarioContext : DbContext
    {
        //public UsuarioContext(DbContextOptions options) : base(options)
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .Property(x => x.UsuarioId)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Usuario>()
                .Property(x => x.fecha_creacion)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Usuario>()
                .Property(x => x.fecha_modificacion)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();


            //modelBuilder.Entity<Usuario>().Property(x => x.UsuarioId).HasDefaultValueSql("NEWSEQUENTIALID()");
            //modelBuilder.Entity<Usuario>().Property(x => x.fecha_creacion).HasDefaultValueSql("GETDATE()");
            //modelBuilder.Entity<Usuario>().Property(x => x.fecha_modificacion).HasDefaultValueSql("GETDATE()");

            base.OnModelCreating(modelBuilder);
        }
    }
}
