using NSubstitute;
using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Application.Services;
using SIGO.Consultorias.Application.TransferObjects;
using SIGO.Consultorias.Application.UseCases.Analises.ConsultaAnalises;
using SIGO.Consultorias.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SIGO.Consultorias.Tests.Application
{
    public class ConsultaAnalisesUseCaseTests
    {
        private readonly ConsultaAnalisesUseCase _consultaAnalisesUseCase;
        private readonly IAnaliseRepository _analiseRepository;
        private readonly IEmpresaUsuarioRepository _empresaUsuarioRepository;
        private readonly IUsuarioAutenticadoService _usuarioAutenticadoService;

        public ConsultaAnalisesUseCaseTests()
        {
            _analiseRepository = Substitute.For<IAnaliseRepository>();
            _empresaUsuarioRepository = Substitute.For<IEmpresaUsuarioRepository>();
            _usuarioAutenticadoService = Substitute.For<IUsuarioAutenticadoService>();
            _consultaAnalisesUseCase = new ConsultaAnalisesUseCase(_analiseRepository, _empresaUsuarioRepository, _usuarioAutenticadoService);
        }

        [Fact]
        public async Task DeveConsultarAnalises()
        {
            var empresa = new Empresa { Id = 1, NomeFantasia = "Consultoria 1" };
            var idUsuario = 99;

            // arrange analise 1
            var analise1 = new Analise
            {
                Id = 1,
                Titulo = "Analise 1",
                Empresa = empresa,
                DataInclusao = DateTime.Now.AddDays(-5),
                DataPublicacao = DateTime.Now.AddDays(-4),
                UsuarioInclusao = "José"
            };

            // arrange analise 2
            var analise2 = new Analise
            {
                Id = 2,
                Titulo = "Analise 2",
                Empresa = empresa,
                DataInclusao = DateTime.Now.AddDays(-6),
                DataAlteracao = DateTime.Now.AddDays(-4),
                UsuarioInclusao = "Maria",
                UsuarioAlteracao = "Pedro"
            };

            _analiseRepository.ConsultarAnalises(empresa.Id).Returns(new List<Analise> { analise1, analise2 });
            _usuarioAutenticadoService.Usuario.Returns(new UsuarioAutenticado { Id = idUsuario, Perfil = "Usuário Externo" });
            _empresaUsuarioRepository.ObterVinculoEmpresaUsuario(idUsuario).Returns(new EmpresaUsuario { EmpresaId = empresa.Id, UsuarioId = idUsuario });

            // act
            var analises = await _consultaAnalisesUseCase.ConsultarAnalises();

            // assert analise 1
            Assert.Equal(analise1.Id, analises.FirstOrDefault().Id);
            Assert.Equal(empresa.NomeFantasia, analises.FirstOrDefault().Empresa);
            Assert.Equal(analise1.Titulo, analises.FirstOrDefault().Titulo);
            Assert.Equal(analise1.DataPublicacao, analises.FirstOrDefault().DataPublicacao);
            Assert.Equal(analise1.UsuarioInclusao, analises.FirstOrDefault().UsuarioUltimaModificacao);
            Assert.Equal(analise1.DataInclusao, analises.FirstOrDefault().DataUltimaEdicao);

            // assert analise 2
            Assert.Equal(analise2.Id, analises.LastOrDefault().Id);
            Assert.Equal(empresa.NomeFantasia, analises.LastOrDefault().Empresa);
            Assert.Equal(analise2.Titulo, analises.LastOrDefault().Titulo);
            Assert.Null(analises.LastOrDefault().DataPublicacao);
            Assert.Equal(analise2.UsuarioAlteracao, analises.LastOrDefault().UsuarioUltimaModificacao);
            Assert.Equal(analise2.DataAlteracao, analises.LastOrDefault().DataUltimaEdicao);
        }
    }
}
