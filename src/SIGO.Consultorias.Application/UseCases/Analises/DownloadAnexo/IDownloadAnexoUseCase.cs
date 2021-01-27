using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.UseCases.Analises.DownloadAnexo
{
    public interface IDownloadAnexoUseCase
    {
        Task<string> ObterDownloadLink(int anexoId);
    }
}
