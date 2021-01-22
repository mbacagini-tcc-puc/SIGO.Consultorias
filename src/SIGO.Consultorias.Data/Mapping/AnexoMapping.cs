using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIGO.Consultorias.Entities;

namespace SIGO.Consultorias.Data.Mapping
{
    public class AnexoMapping : BaseEntityMapping<Anexo>
    {
        public override void Configure(EntityTypeBuilder<Anexo> builder)
        {
            builder.ToTable("anexos");
            builder.Property(prop => prop.AnaliseId).HasColumnName("id_analise");
            builder.Property(prop => prop.NomeArquivo).HasColumnName("nome_arquivo").HasMaxLength(256);
            builder.Property(prop => prop.Caminho).HasColumnName("caminho").HasMaxLength(500);
            builder.HasOne(prop => prop.Analise).WithMany(prop => prop.Anexos).HasForeignKey(prop => prop.AnaliseId);
            base.Configure(builder);
        }
    }
}
