using NSubstitute;
using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Application.Services;
using SIGO.Consultorias.Application.TransferObjects;
using SIGO.Consultorias.Application.UseCases.Analises.DetalhamentoAnalise;
using SIGO.Consultorias.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SIGO.Consultorias.Tests.Application
{
    public class DetalhamentoAnaliseUseCaseTests
    {
        private readonly DetalhamentoAnaliseUseCase _detalhamentoAnaliseUseCase;
        private readonly IAnaliseRepository _analiseRepository;
        private readonly IEmpresaUsuarioRepository _empresaUsuarioRepository;
        private readonly IUsuarioAutenticadoService _usuarioAutenticadoService;

        public DetalhamentoAnaliseUseCaseTests()
        {
            _analiseRepository = Substitute.For<IAnaliseRepository>();
            _empresaUsuarioRepository = Substitute.For<IEmpresaUsuarioRepository>();
            _usuarioAutenticadoService = Substitute.For<IUsuarioAutenticadoService>();
            _detalhamentoAnaliseUseCase = new DetalhamentoAnaliseUseCase(_analiseRepository, _usuarioAutenticadoService, _empresaUsuarioRepository);
        }

        [Fact]
        public async Task DeveRetornarNullSeNaoExistirAnalise()
        {
            // arrange
            _analiseRepository.ObterAnaliseDetalhada(1).Returns((Analise)null);

            // act
            var resultado = await _detalhamentoAnaliseUseCase.ObterDetalhesAnalise(1);

            // assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task DeveRetornarNullSeAnaliseNaoPertenceEmpresaUsuarioAutenticado()
        {
            // arrange
            var empresaUsuario = 99;
            var empresaAnalise = 100;
            var usuarioAutenticado = new UsuarioAutenticado { Id = 50, Perfil = "Usuário Externo" };

            _usuarioAutenticadoService.Usuario.Returns(usuarioAutenticado);
            _empresaUsuarioRepository.ObterVinculoEmpresaUsuario(usuarioAutenticado.Id).Returns(new EmpresaUsuario { EmpresaId = empresaUsuario });
            _analiseRepository.ObterAnaliseDetalhada(1).Returns(new Analise { EmpresaId = empresaAnalise });

            // act
            var resultado = await _detalhamentoAnaliseUseCase.ObterDetalhesAnalise(1);

            // assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task DeveRetornarDetalhesAnalise()
        {
            var usuarioAutenticado = new UsuarioAutenticado { Id = 1, Perfil = "Usuário Interno" };
            var analise = new Analise
            {
                Id = 1,
                Titulo = "Analise 1",
                Resumo = "Resumo",
                Empresa = new Empresa { NomeFantasia = "Consultoria 1" },
                DataInclusao = DateTime.Now.AddDays(-5),
                DataAlteracao = DateTime.Now.AddDays(-3),
                DataPublicacao = DateTime.Now.AddDays(-2),
                UsuarioInclusao = "José",
                UsuarioAlteracao = "Maria",
                Anexos = new List<Anexo>
                {
                   new Anexo { Id = 101, NomeArquivo = "Analise1.pdf", DataInclusao = DateTime.Now.AddDays(-3)}
                }
            };

            _analiseRepository.ObterAnaliseDetalhada(1).Returns(analise);
            _usuarioAutenticadoService.Usuario.Returns(usuarioAutenticado);

            // act
            var resultado = await _detalhamentoAnaliseUseCase.ObterDetalhesAnalise(1);

            // arrange
            Assert.Equal(analise.Id, resultado.Id);
            Assert.Equal(analise.Titulo, resultado.Titulo);
            Assert.Equal(analise.Resumo, resultado.Resumo);
            Assert.Equal(analise.Empresa.NomeFantasia, resultado.Empresa);
            Assert.Equal(analise.DataInclusao, resultado.DataInclusao);
            Assert.Equal(analise.DataAlteracao, resultado.DataAlteracao);
            Assert.Equal(analise.UsuarioInclusao, resultado.UsuarioInclusao);
            Assert.Equal(analise.UsuarioAlteracao, resultado.UsuarioAlteracao);
            Assert.Equal(analise.Anexos.FirstOrDefault().Id, resultado.Anexos.FirstOrDefault().Id);
            Assert.Equal(analise.Anexos.FirstOrDefault().NomeArquivo, resultado.Anexos.FirstOrDefault().NomeArquivo);
            Assert.Equal(analise.Anexos.FirstOrDefault().DataInclusao, resultado.Anexos.FirstOrDefault().DataInclusao);
        }
    }
}
