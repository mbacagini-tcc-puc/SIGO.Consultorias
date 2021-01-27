using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGO.Consultorias.Application.UseCases.Analises.DownloadAnexo;
using SIGO.Consultorias.Application.UseCases.Analises.ExclusaoAnexo;
using SIGO.Consultorias.Application.UseCases.Analises.InclusaoAnexo;
using System.Threading.Tasks;

namespace SIGO.Consultorias.API.Controllers
{
    [ApiController]
    public class AnexosController : ControllerBase
    {
        private readonly IInclusaoAnexoUseCase _inclusaoAnexoUseCase;
        private readonly IExclusaoAnexoUseCase _exclusaoAnexoUseCase;
        private readonly IDownloadAnexoUseCase _downloadAnexoUseCase;

        public AnexosController(IInclusaoAnexoUseCase inclusaoAnexoUseCase, IExclusaoAnexoUseCase exclusaoAnexoUseCase, IDownloadAnexoUseCase downloadAnexoUseCase)
        {
            _inclusaoAnexoUseCase = inclusaoAnexoUseCase;
            _exclusaoAnexoUseCase = exclusaoAnexoUseCase;
            _downloadAnexoUseCase = downloadAnexoUseCase;
        }

        [Route("analises/{analiseId}/anexos")]
        [HttpPost]
        public async Task<IActionResult> SalvarAnexo(int analiseId, [FromForm] IFormFile anexo)
        {
            await _inclusaoAnexoUseCase.IncluirAnexo(analiseId, anexo.FileName, anexo.OpenReadStream());

            return Ok();
        }

        [Route("analises/{analiseId}/anexos/{anexoId}")]
        [HttpDelete]
        public async Task<IActionResult> SalvarAnexo(int anexoId)
        {
            await _exclusaoAnexoUseCase.ExcluirAnexo(anexoId);

            return Ok();
        }

        [Route("analises/{analiseId}/anexos/{anexoId}/download")]
        [HttpGet]
        public async Task<IActionResult> Download(int anexoId)
        {
            var linkDownload = await _downloadAnexoUseCase.ObterDownloadLink(anexoId);

            return Ok(new { url = linkDownload });
        }
    }
}