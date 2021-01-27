using SIGO.Consultorias.Entities;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.Repositories
{
    public interface IAnexoRepository
    {
        Task<Anexo> ObterPorId(int id);
        Task Inserir(Anexo anexo);
        Task Excluir(Anexo anexo);
    }
}
