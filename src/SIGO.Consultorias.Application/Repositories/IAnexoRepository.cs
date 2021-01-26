using SIGO.Consultorias.Entities;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.Repositories
{
    public interface IAnexoRepository
    {
        Task Inserir(Anexo anexo);
    }
}
