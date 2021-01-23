using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.UseCases.Analises.EdicaoAnalise
{
    public interface IEdicaoAnaliseUseCase
    {
        Task<int> SalvarAnalise(EdicaoAnaliseInput analiseInput);
    }
}
