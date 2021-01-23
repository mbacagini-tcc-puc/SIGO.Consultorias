using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public AnalisesController(IEdicaoAnaliseUseCase edicaoAnaliseUseCase)
        {
            _edicaoAnaliseUseCase = edicaoAnaliseUseCase;
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
    }
}