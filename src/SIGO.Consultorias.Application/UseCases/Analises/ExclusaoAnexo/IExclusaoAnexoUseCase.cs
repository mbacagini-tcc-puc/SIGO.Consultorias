using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.UseCases.Analises.ExclusaoAnexo
{
    public interface IExclusaoAnexoUseCase
    {
        Task ExcluirAnexo(int id);
    }
}
