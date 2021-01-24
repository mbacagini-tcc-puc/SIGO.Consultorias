using System;
using System.Collections.Generic;

namespace SIGO.Consultorias.Application.UseCases.Analises.DetalhamentoAnalise
{
    public class DetalhamentoAnaliseOutput
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public DateTimeOffset DataInclusao { get; set; }
        public DateTimeOffset? DataAlteracao { get; set; }
        public DateTimeOffset? DataPublicacao { get; set; }
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public IEnumerable<AnexoOutput> Anexos { get; set; }

    }
}
