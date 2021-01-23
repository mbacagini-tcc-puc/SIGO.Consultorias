using System;

namespace SIGO.Consultorias.Application.UseCases.Analises.EdicaoAnalise
{
    public class EdicaoAnaliseInput
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public DateTimeOffset? DataPublicacao { get; set; }
    }
}
