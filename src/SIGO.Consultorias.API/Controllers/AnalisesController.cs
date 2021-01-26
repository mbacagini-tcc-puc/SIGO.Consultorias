using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIGO.Consultorias.Application.UseCases.Analises.ConsultaAnalises;
using SIGO.Consultorias.Application.UseCases.Analises.DetalhamentoAnalise;
using SIGO.Consultorias.Application.UseCases.Analises.EdicaoAnalise;
using System.Net;
using System.Threading.Tasks;

namespace SIGO.Consultorias.API.Controllers
{
    [Authorize("Bearer")]
    [Route("[controller]")]
    [ApiController]
    public class AnalisesController : ControllerBase
    {
        private readonly IEdicaoAnaliseUseCase _edicaoAnaliseUseCase;
        private readonly IConsultaAnalisesUseCase _consultaAnalisesUseCase;
        private readonly IDetalhamentoAnaliseUseCase _detalhamentoAnaliseUseCase;

        public AnalisesController(IEdicaoAnaliseUseCase edicaoAnaliseUseCase,
                                  IConsultaAnalisesUseCase consultaAnalisesUseCase, 
                                  IDetalhamentoAnaliseUseCase detalhamentoAnaliseUseCase)
        {
            _edicaoAnaliseUseCase = edicaoAnaliseUseCase;
            _consultaAnalisesUseCase = consultaAnalisesUseCase;
            _detalhamentoAnaliseUseCase = detalhamentoAnaliseUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> InserirAnalise([FromBody] EdicaoAnaliseInput analiseInput)
        {
            var idNovaAnalise = await _edicaoAnaliseUseCase.SalvarAnalise(analiseInput);

            return StatusCode((int)HttpStatusCode.Created, idNovaAnalise);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> AtualizarAnalise(int id, [FromBody] EdicaoAnaliseInput analiseInput)
        {
            analiseInput.Id = id;

            await _edicaoAnaliseUseCase.SalvarAnalise(analiseInput);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarAnalises()
        {
            var analises = await _consultaAnalisesUseCase.ConsultarAnalises();

            return Ok(analises);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> DetalharAnalise(int id)
        {
            var analise = await _detalhamentoAnaliseUseCase.ObterDetalhesAnalise(id);

            if(analise == null)
            {
                return NotFound();
            }

            return Ok(analise);
        }
    }
}