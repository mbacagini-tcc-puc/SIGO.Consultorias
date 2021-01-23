using Microsoft.EntityFrameworkCore;
using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Entities;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Data.Repositories
{
    public class EmpresaUsuarioRepository : IEmpresaUsuarioRepository
    {
        private readonly ConsultoriasContext _context;

        public EmpresaUsuarioRepository(ConsultoriasContext context)
        {
            _context = context;
        }

        public async Task<EmpresaUsuario> ObterPorUsuario(int usuarioId)
        {
            return await _context.EmpresasUsuarios.AsNoTracking().FirstOrDefaultAsync(empresaUsuario => empresaUsuario.UsuarioId == usuarioId);
        }
    }
}
