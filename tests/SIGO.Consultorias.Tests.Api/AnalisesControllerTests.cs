using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SIGO.Consultorias.API.Controllers;
using SIGO.Consultorias.Application.UseCases.Analises.ConsultaAnalises;
using SIGO.Consultorias.Application.UseCases.Analises.EdicaoAnalise;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SIGO.Consultorias.Tests.Api
{
    public class AnalisesControllerTests
    {
        private readonly AnalisesController _analisesController;
        private readonly IEdicaoAnaliseUseCase _edicaoAnaliseUseCase;
        private readonly IConsultaAnalisesUseCase _consultaAnalisesUseCase;

        public AnalisesControllerTests()
        {
            _edicaoAnaliseUseCase = Substitute.For<IEdicaoAnaliseUseCase>();
            _consultaAnalisesUseCase = Substitute.For<IConsultaAnalisesUseCase>();
            _analisesController = new AnalisesController(_edicaoAnaliseUseCase, _consultaAnalisesUseCase);
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

        [Fact]
        public async Task DeveConsultarAnalises()
        {
            // arrange
            var analises = new List<ConsultaAnaliseOutput>();

            _consultaAnalisesUseCase.ConsultarAnalises().Returns(analises);

            // act
            var resultado = await _analisesController.ConsultarAnalises();

            // assert
            Assert.IsType<OkObjectResult>(resultado);
            Assert.Equal(analises, ((OkObjectResult)resultado).Value);
        }
    }
}
