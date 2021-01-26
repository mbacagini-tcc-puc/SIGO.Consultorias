using NSubstitute;
using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Application.Services;
using SIGO.Consultorias.Application.UseCases.Analises.InclusaoAnexo;
using SIGO.Consultorias.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SIGO.Consultorias.Tests.Application
{
    public class InclusaoAnexoUseCaseTests
    {
        private readonly InclusaoAnexoUseCase _inclusaoAnexoUseCase;
        private readonly IAnexoRepository _anexoRepository;
        private readonly IFileService _fileService;

        public InclusaoAnexoUseCaseTests()
        {
            _anexoRepository = Substitute.For<IAnexoRepository>();
            _fileService = Substitute.For<IFileService>();
            _inclusaoAnexoUseCase = new InclusaoAnexoUseCase(_anexoRepository, _fileService);
        }

        [Fact]
        public async Task DeveIncluirAnexo()
        {
            var analiseId = 1;
            var nomeAnexo = "arquivo.pdf";
            var stream = new MemoryStream();

            Anexo anexoIncluido = null;

            await _anexoRepository.Inserir(Arg.Do<Anexo>(recebido => anexoIncluido = recebido));

            // act
            await _inclusaoAnexoUseCase.IncluirAnexo(analiseId, nomeAnexo, stream);

            // assert
            Assert.Equal(analiseId, anexoIncluido.AnaliseId);
            Assert.Equal(nomeAnexo, anexoIncluido.NomeArquivo);
            Assert.EndsWith(nomeAnexo, anexoIncluido.Caminho);

            await _fileService.Received(1).Upload(stream, anexoIncluido.Caminho);

            // dispose
            stream.Dispose();
        }
    }
}
