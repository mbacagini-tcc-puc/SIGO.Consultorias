using NSubstitute;
using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Application.Services;
using SIGO.Consultorias.Application.TransferObjects;
using SIGO.Consultorias.Application.UseCases.Analises.EdicaoAnalise;
using SIGO.Consultorias.Entities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SIGO.Consultorias.Tests.Application
{
    public class EdicaoAnaliseUseCaseTests
    {
        private readonly EdicaoAnaliseUseCase _edicaoAnaliseUseCase;
        private readonly IAnaliseRepository _analiseRepository;
        private readonly IEmpresaUsuarioRepository _empresaUsuarioRepository;
        private readonly IUsuarioAutenticadoService _usuarioAutenticadoService;

        private const string NomeUsuarioAutenticado = "José";
        private const int IdUsuarioAutenticado = 1;
        private const int EmpresaId = 2;

        public EdicaoAnaliseUseCaseTests()
        {
            _analiseRepository = Substitute.For<IAnaliseRepository>();
            _empresaUsuarioRepository = Substitute.For<IEmpresaUsuarioRepository>();
            _usuarioAutenticadoService = Substitute.For<IUsuarioAutenticadoService>();

            _usuarioAutenticadoService.Usuario.Returns(new UsuarioAutenticado
            {
                Id = IdUsuarioAutenticado,
                Nome = NomeUsuarioAutenticado
            });

            _empresaUsuarioRepository.ObterVinculoEmpresaUsuario(IdUsuarioAutenticado).Returns(new EmpresaUsuario
            {
                EmpresaId = EmpresaId
            });

            _edicaoAnaliseUseCase = new EdicaoAnaliseUseCase(_analiseRepository, _empresaUsuarioRepository, _usuarioAutenticadoService);
        }

        [Fact]
        public async Task DeveInserirAnalise()
        {
            var titulo = "Titulo";
            var resumo = "Resumo";
            var input = new EdicaoAnaliseInput
            {
                Titulo = titulo,
                Resumo = resumo
            };

            Analise analiseCriada = null;

            await _analiseRepository.Inserir(Arg.Do<Analise>(recebido => analiseCriada = recebido));

            // act
            await _edicaoAnaliseUseCase.SalvarAnalise(input);

            // assert
            Assert.Equal(titulo, analiseCriada.Titulo);
            Assert.Equal(resumo, analiseCriada.Resumo);
            Assert.Equal(EmpresaId, analiseCriada.EmpresaId);
            Assert.Equal(IdUsuarioAutenticado, analiseCriada.UsuarioInclusaoId);
            Assert.Equal(NomeUsuarioAutenticado, analiseCriada.UsuarioInclusao);
            Assert.Null(analiseCriada.UsuarioAlteracaoId);
            Assert.Null(analiseCriada.UsuarioAlteracao);
            Assert.Null(analiseCriada.DataAlteracao);
        }

        [Fact]
        public async Task DeveAtualizarAnalise()
        {
            var titulo = "Titulo alterado";
            var resumo = "Resumo alterado";
            var id = 1;
            var input = new EdicaoAnaliseInput
            {
                Id = id,
                Titulo = titulo,
                Resumo = resumo,
                DataPublicacao = DateTime.UtcNow
            };

            var analise = new Analise
            {
                Id = id,
                Titulo = "Titulo",
                Resumo = "Resumo",
                EmpresaId = EmpresaId,
                UsuarioInclusaoId = IdUsuarioAutenticado,
                UsuarioInclusao = NomeUsuarioAutenticado
            };

            _analiseRepository.ObterAnalisePorId(id).Returns(analise);

            // act
            await _edicaoAnaliseUseCase.SalvarAnalise(input);

            // assert
            Assert.Equal(titulo, analise.Titulo);
            Assert.Equal(resumo, analise.Resumo);
            Assert.Equal(EmpresaId, analise.EmpresaId);
            Assert.Equal(IdUsuarioAutenticado, analise.UsuarioInclusaoId);
            Assert.Equal(NomeUsuarioAutenticado, analise.UsuarioInclusao);
            Assert.Equal(IdUsuarioAutenticado, analise.UsuarioAlteracaoId);
            Assert.Equal(NomeUsuarioAutenticado, analise.UsuarioAlteracao);
            Assert.NotNull(analise.DataAlteracao);

            await _analiseRepository.Received(1).Atualizar(analise);
        }
    }
}
