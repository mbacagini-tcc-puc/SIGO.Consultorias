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


        public async Task Inserir(Anexo anexo)
        {
            await _context.AddAsync(anexo);
            await _context.SaveChangesAsync();
        }
    }
}
