using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SIGO.Consultorias.API.Controllers;
using SIGO.Consultorias.Application.UseCases.Analises.EdicaoAnalise;
using System.Threading.Tasks;
using Xunit;

namespace SIGO.Consultorias.Tests.Api
{
    public class AnalisesControllerTests
    {
        private readonly AnalisesController _analisesController;
        private readonly IEdicaoAnaliseUseCase _edicaoAnaliseUseCase;

        public AnalisesControllerTests()
        {
            _edicaoAnaliseUseCase = Substitute.For<IEdicaoAnaliseUseCase>();
            _analisesController = new AnalisesController(_edicaoAnaliseUseCase);
        }

        [Fact]
        public async Task DeveCriarAnalise()
        {
            // arrange
            var input = new EdicaoAnaliseInput();

            _edicaoAnaliseUseCase.SalvarAnalise(input).Returns(1);

            // act
            var resultado = await _analisesController.InserirAnalise(input);

            // assert
            Assert.IsType<ObjectResult>(resultado);
            Assert.Equal(201, ((ObjectResult)resultado).StatusCode);
            Assert.Equal(1, ((ObjectResult)resultado).Value);
        }

        [Fact]
        public async Task DeveAtualizarAnalise()
        {
            // arrange
            var input = new EdicaoAnaliseInput();

            // act
            var resultado = await _analisesController.AtualizarAnalise(2, input);

            // assert
            Assert.IsType<OkResult>(resultado);
            Assert.Equal(2, input.Id);
        }
    }
}
