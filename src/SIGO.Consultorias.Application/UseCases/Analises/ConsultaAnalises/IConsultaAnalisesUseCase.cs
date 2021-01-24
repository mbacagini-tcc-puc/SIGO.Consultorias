using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.UseCases.Analises.ConsultaAnalises
{
    public interface IConsultaAnalisesUseCase
    {
        public Task<IEnumerable<ConsultaAnaliseOutput>> ConsultarAnalises();
    }
}
