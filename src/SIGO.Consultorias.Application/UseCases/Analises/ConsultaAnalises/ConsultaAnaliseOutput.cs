using System;

namespace SIGO.Consultorias.Application.UseCases.Analises.ConsultaAnalises
{
    public class ConsultaAnaliseOutput
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string Titulo { get; set; }
        public DateTimeOffset? DataPublicacao { get; set; }
        public DateTimeOffset DataUltimaEdicao { get; set; }
        public string UsuarioUltimaModificacao { get; set; }
    }
}
