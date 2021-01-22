using System;
using System.Collections.Generic;

namespace SIGO.Consultorias.Entities
{
    public class Analise : BaseEntity
    {
        public int EmpresaId { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public int UsuarioInclusaoId { get; set; }
        public string UsuarioInclusao { get; set; }
        public int? UsuarioAlteracaoId { get; set; }
        public string UsuarioAlteracao { get; set; }
        public DateTimeOffset? DataPublicacao { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<Anexo> Anexos { get; set; }
    }
}
