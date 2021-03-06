﻿using Microsoft.EntityFrameworkCore;
using SIGO.Consultorias.Data.Mapping;
using SIGO.Consultorias.Entities;

namespace SIGO.Consultorias.Data
{
    public class ConsultoriasContext : DbContext
    {
        public ConsultoriasContext(DbContextOptions<ConsultoriasContext> options) : base(options)
        {

        }

        public DbSet<Analise> Analises { get; set; }
        public DbSet<Anexo> Anexos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<EmpresaUsuario> EmpresasUsuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Analise>(new AnaliseMapping().Configure);
            modelBuilder.Entity<Anexo>(new AnexoMapping().Configure);
            modelBuilder.Entity<Empresa>(new EmpresaMapping().Configure);
            modelBuilder.Entity<EmpresaUsuario>(new EmpresaUsuarioMapping().Configure);
        }
    }
}
