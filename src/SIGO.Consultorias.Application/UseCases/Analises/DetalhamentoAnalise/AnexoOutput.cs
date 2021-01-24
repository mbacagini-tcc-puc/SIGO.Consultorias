using System;

namespace SIGO.Consultorias.Application.UseCases.Analises.DetalhamentoAnalise
{
    public class AnexoOutput
    {
        public int Id { get; set; }
        public string NomeArquivo { get; set; }
        public DateTimeOffset DataInclusao { get; set; }
    }
}
