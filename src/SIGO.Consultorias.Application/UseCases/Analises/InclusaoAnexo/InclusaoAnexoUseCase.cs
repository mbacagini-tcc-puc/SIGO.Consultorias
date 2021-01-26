using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Application.Services;
using SIGO.Consultorias.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.UseCases.Analises.InclusaoAnexo
{
    public class InclusaoAnexoUseCase : IInclusaoAnexoUseCase
    {
        private readonly IAnexoRepository _anexoRepository;
        private readonly IFileService _fileService;

        public InclusaoAnexoUseCase(IAnexoRepository anexoRepository, IFileService fileService)
        {
            _anexoRepository = anexoRepository;
            _fileService = fileService;
        }

        public async Task IncluirAnexo(int analiseId, string nomeArquivo, Stream fileStream)
        {
            var anexo = new Anexo
            {
                 AnaliseId = analiseId,
                 NomeArquivo = nomeArquivo,
                 Caminho = $"{DateTime.Now:ddMMyyyyHHmmss}_{nomeArquivo}",
                 DataInclusao = DateTime.UtcNow
            };

            await _fileService.Upload(fileStream, anexo.Caminho);
            await _anexoRepository.Inserir(anexo);
        }
    }
}
