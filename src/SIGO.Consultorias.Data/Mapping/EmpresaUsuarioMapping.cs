using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIGO.Consultorias.Entities;
using System;

namespace SIGO.Consultorias.Data.Mapping
{
    public class EmpresaUsuarioMapping : BaseEntityMapping<EmpresaUsuario>
    {
        public override void Configure(EntityTypeBuilder<EmpresaUsuario> builder)
        {
            builder.ToTable("empresas_usuarios");
            builder.Property(prop => prop.EmpresaId).HasColumnName("id_empresa");
            builder.Property(prop => prop.UsuarioId).HasColumnName("id_usuario");
            builder.HasOne(prop => prop.Empresa).WithMany(prop => prop.Usuarios).HasForeignKey(prop => prop.EmpresaId);
            base.Configure(builder);

            builder.HasData(new EmpresaUsuario[]
            {
                new EmpresaUsuario { Id = 1,  EmpresaId = 1 , UsuarioId = 2, DataInclusao = DateTime.UtcNow },
                new EmpresaUsuario { Id = 2,  EmpresaId = 2 , UsuarioId = 3, DataInclusao = DateTime.UtcNow }
            });
        }
    }
}
