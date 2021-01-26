using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGO.Consultorias.Application.UseCases.Analises.InclusaoAnexo;
using System.Threading.Tasks;

namespace SIGO.Consultorias.API.Controllers
{
    [ApiController]
    public class AnexosController : ControllerBase
    {
        private readonly IInclusaoAnexoUseCase _inclusaoAnexoUseCase;

        public AnexosController(IInclusaoAnexoUseCase inclusaoAnexoUseCase)
        {
            _inclusaoAnexoUseCase = inclusaoAnexoUseCase;
        }

        [Route("analises/{analiseId}/anexos")]
        [HttpPost]
        public async Task<IActionResult> SalvarAnexo(int analiseId, [FromForm] IFormFile anexo)
        {
            await _inclusaoAnexoUseCase.IncluirAnexo(analiseId, anexo.FileName, anexo.OpenReadStream());

            return Ok();
        }
    }
}