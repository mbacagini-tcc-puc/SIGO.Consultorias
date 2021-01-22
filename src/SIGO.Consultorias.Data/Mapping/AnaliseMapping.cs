using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIGO.Consultorias.Entities;

namespace SIGO.Consultorias.Data.Mapping
{
    public class AnaliseMapping : BaseEntityMapping<Analise>
    {
        public override void Configure(EntityTypeBuilder<Analise> builder)
        {
            builder.ToTable("analises");
            builder.Property(prop => prop.EmpresaId).HasColumnName("id_empresa");
            builder.Property(prop => prop.UsuarioInclusaoId).HasColumnName("id_usuario_inclusao");
            builder.Property(prop => prop.UsuarioAlteracaoId).HasColumnName("id_usuario_alteracao");
            builder.Property(prop => prop.UsuarioInclusao).HasColumnName("usuario_inclusao").HasMaxLength(100);
            builder.Property(prop => prop.UsuarioAlteracao).HasColumnName("usuario_alteracao").HasMaxLength(100);
            builder.Property(prop => prop.Titulo).HasColumnName("titulo").HasMaxLength(50);
            builder.Property(prop => prop.Resumo).HasColumnName("resumo").HasMaxLength(500);
            builder.Property(prop => prop.DataPublicacao).HasColumnName("data_publicacao");
            builder.HasOne(prop => prop.Empresa).WithMany(prop => prop.Analises).HasForeignKey(prop => prop.EmpresaId);
            base.Configure(builder);
        }
    }
}
