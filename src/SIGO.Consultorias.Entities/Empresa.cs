using System.Collections;
using System.Collections.Generic;

namespace SIGO.Consultorias.Entities
{
    public class Empresa : BaseEntity
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public bool ContratoAtivo { get; set; }
        public virtual ICollection<Analise> Analises { get; set; }
        public virtual ICollection<EmpresaUsuario> Usuarios { get; set; }
    }
}
