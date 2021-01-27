using NSubstitute;
using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Application.Services;
using SIGO.Consultorias.Application.TransferObjects;
using SIGO.Consultorias.Application.UseCases.Analises.DownloadAnexo;
using SIGO.Consultorias.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SIGO.Consultorias.Tests.Application
{
    public class DownloadAnexoUseCaseTests
    {
        private readonly DownloadAnexoUseCase _downloadAnexoUseCase;
        private readonly IAnexoRepository _anexoRepository;
        private readonly IFileService _fileService;
        private readonly IEmpresaUsuarioRepository _empresaUsuarioRepository;
        private readonly IUsuarioAutenticadoService _usuarioAutenticadoService;

        public DownloadAnexoUseCaseTests()
        {
            _anexoRepository = Substitute.For<IAnexoRepository>();
            _fileService = Substitute.For<IFileService>();
            _empresaUsuarioRepository = Substitute.For<IEmpresaUsuarioRepository>();
            _usuarioAutenticadoService = Substitute.For<IUsuarioAutenticadoService>();
            _downloadAnexoUseCase = new DownloadAnexoUseCase(_anexoRepository, _fileService, _empresaUsuarioRepository, _usuarioAutenticadoService);
        }

        [Fact]
        public async Task DeveGerarLinkDownload()
        {
            var anexoId = 1;
            var usuarioId = 454;
            var empresaId = 99;
            var nomeArquivo = "arquivo.pdf";
            var linkDownload = "link";

            _anexoRepository.ObterPorId(anexoId).Returns(new Anexo
            {
                 Id = anexoId,
                 Caminho = nomeArquivo,
                 Analise = new Analise { EmpresaId = empresaId }
            });

            _empresaUsuarioRepository.ObterVinculoEmpresaUsuario(usuarioId).Returns(new EmpresaUsuario { EmpresaId = empresaId, UsuarioId = usuarioId });
            _usuarioAutenticadoService.Usuario.Returns(new UsuarioAutenticado { Id = usuarioId, Perfil = "Usuário Externo" });
            _fileService.ObterDownloadLink(nomeArquivo).Returns(linkDownload);

            // act
            var linkGerado = await _downloadAnexoUseCase.ObterDownloadLink(anexoId);

            // assert
            Assert.Equal(linkDownload, linkGerado);
        }

        [Fact]
        public async Task NaoDeveGerarLinkDownload()
        {
            var anexoId = 1;
            var usuarioId = 454;
            var empresaUsuarioId = 2;
            var empresaAnexoId = 99;
            
            _anexoRepository.ObterPorId(anexoId).Returns(new Anexo
            {
                Id = anexoId,
                Analise = new Analise { EmpresaId = empresaAnexoId }
            });

            _empresaUsuarioRepository.ObterVinculoEmpresaUsuario(usuarioId).Returns(new EmpresaUsuario { EmpresaId = empresaUsuarioId, UsuarioId = usuarioId });
            _usuarioAutenticadoService.Usuario.Returns(new UsuarioAutenticado { Id = usuarioId, Perfil = "Usuário Externo" });

            // act
            var linkGerado = await _downloadAnexoUseCase.ObterDownloadLink(anexoId);

            // assert
            Assert.Null(linkGerado);
        }
    }
}
