using NSubstitute;
using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Application.Services;
using SIGO.Consultorias.Application.UseCases.Analises.ExclusaoAnexo;
using SIGO.Consultorias.Entities;
using System.Threading.Tasks;
using Xunit;

namespace SIGO.Consultorias.Tests.Application
{
    public class ExclusaoAnexoUseCaseTests
    {
        private readonly ExclusaoAnexoUseCase _exclusaoAnexoUseCase;
        private readonly IAnexoRepository _anexoRepository;
        private readonly IFileService _fileService;

        public ExclusaoAnexoUseCaseTests()
        {
            _anexoRepository = Substitute.For<IAnexoRepository>();
            _fileService = Substitute.For<IFileService>();
            _exclusaoAnexoUseCase = new ExclusaoAnexoUseCase(_anexoRepository, _fileService);
        }

        [Fact]
        public async Task DeveExcluirAnexo()
        {
            // arrange
            var id = 1;
            var path = "arquivo.pdf";
            var anexo = new Anexo { Id = id, Caminho = path };

            _anexoRepository.ObterPorId(id).Returns(anexo);

            // act
            await _exclusaoAnexoUseCase.ExcluirAnexo(id);

            // assert
            await _anexoRepository.Received(1).Excluir(anexo);
            await _fileService.Received(1).Excluir(path);
        }
    }
}
