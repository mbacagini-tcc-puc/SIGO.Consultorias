using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Application.Services;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.UseCases.Analises.ExclusaoAnexo
{
    public class ExclusaoAnexoUseCase : IExclusaoAnexoUseCase
    {
        private readonly IAnexoRepository _anexoRepository;
        private readonly IFileService _fileService;

        public ExclusaoAnexoUseCase(IAnexoRepository anexoRepository, IFileService fileService)
        {
            _anexoRepository = anexoRepository;
            _fileService = fileService;
        }

        public async Task ExcluirAnexo(int id)
        {
            var anexo = await _anexoRepository.ObterPorId(id);
            await _fileService.Excluir(anexo.Caminho);
            await _anexoRepository.Excluir(anexo);
        }
    }
}
