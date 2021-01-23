using SIGO.Consultorias.Entities;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.Repositories
{
    public interface IEmpresaUsuarioRepository
    {
        Task<EmpresaUsuario> ObterPorUsuario(int usuarioId);
    }
}
