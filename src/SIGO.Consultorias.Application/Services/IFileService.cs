using System.IO;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.Services
{
    public interface IFileService
    {
        Task Upload(Stream file, string fileName);
        Task Excluir(string fileName);
    }
}
