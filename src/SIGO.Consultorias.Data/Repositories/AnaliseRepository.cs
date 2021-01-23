using Microsoft.EntityFrameworkCore;
using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Entities;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Data.Repositories
{
    public class AnaliseRepository : IAnaliseRepository
    {
        private readonly ConsultoriasContext _context;

        public AnaliseRepository(ConsultoriasContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Analise analise)
        {
            _context.Entry(analise).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Inserir(Analise analise)
        {
            await _context.AddAsync(analise);
            await _context.SaveChangesAsync();
        }

        public async Task<Analise> ObterAnalisePorId(int id)
        {
            return await _context.Analises.FirstOrDefaultAsync(analise => analise.Id == id);
        }
    }
}
