using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.UseCases.Analises.DetalhamentoAnalise
{
    public interface IDetalhamentoAnaliseUseCase
    {
        Task<DetalhamentoAnaliseOutput> ObterDetalhesAnalise(int analiseId);
    }
}
