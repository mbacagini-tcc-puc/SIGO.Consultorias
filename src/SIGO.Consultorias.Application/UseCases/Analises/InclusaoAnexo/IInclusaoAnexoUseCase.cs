using System.IO;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.UseCases.Analises.InclusaoAnexo
{
    public interface IInclusaoAnexoUseCase
    {
        Task IncluirAnexo(int analiseId, string nomeArquivo, Stream fileStream);
    }
}
