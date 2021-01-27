using Microsoft.EntityFrameworkCore;
using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Entities;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Data.Repositories
{
    public class AnexoRepository : IAnexoRepository
    {
        private readonly ConsultoriasContext _context;

        public AnexoRepository(ConsultoriasContext context)
        {
            _context = context;
        }

        public async Task Excluir(Anexo anexo)
        {
            _context.Remove(anexo);
            await _context.SaveChangesAsync();
        }

        public async Task Inserir(Anexo anexo)
        {
            await _context.AddAsync(anexo);
            await _context.SaveChangesAsync();
        }

        public async Task<Anexo> ObterPorId(int id)
        {
            return await _context.Anexos.Include(anexo => anexo.Analise).FirstOrDefaultAsync(anexo => anexo.Id == id);
        }
    }
}
