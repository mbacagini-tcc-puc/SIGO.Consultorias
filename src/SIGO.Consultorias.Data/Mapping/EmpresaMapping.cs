using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIGO.Consultorias.Entities;
using System;

namespace SIGO.Consultorias.Data.Mapping
{
    public class EmpresaMapping : BaseEntityMapping<Empresa>
    {
        public override void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("empresas");
            builder.Property(prop => prop.NomeFantasia).HasColumnName("nome_fantasia").HasMaxLength(30);
            builder.Property(prop => prop.RazaoSocial).HasColumnName("razao_social").HasMaxLength(100);
            builder.Property(prop => prop.ContratoAtivo).HasColumnName("contrato_ativo").HasMaxLength(18);
            base.Configure(builder);

            builder.HasData(new Empresa[]
            {
                new Empresa
                {
                    Id = 1,
                    RazaoSocial = "Consultoria 1 LTDA",
                    NomeFantasia = "Consultoria 1",
                    ContratoAtivo = true,
                    DataInclusao = DateTime.UtcNow
                },

                new Empresa
                {
                    Id = 2,
                    RazaoSocial = "Consultoria 2 LTDA",
                    NomeFantasia = "Consultoria 2",
                    ContratoAtivo = true,
                    DataInclusao = DateTime.UtcNow
                }
            });
        }
    }
}
