using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Application.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.UseCases.Analises.ConsultaAnalises
{
    public class ConsultaAnalisesUseCase : IConsultaAnalisesUseCase
    {
        private readonly IAnaliseRepository _analiseRepository;
        private readonly IEmpresaUsuarioRepository _empresaUsuarioRepository;
        private readonly IUsuarioAutenticadoService _usuarioAutenticadoService;

        public ConsultaAnalisesUseCase(IAnaliseRepository analiseRepository,
                                       IEmpresaUsuarioRepository empresaUsuarioRepository,
                                       IUsuarioAutenticadoService usuarioAutenticadoService)
        {
            _analiseRepository = analiseRepository;
            _empresaUsuarioRepository = empresaUsuarioRepository;
            _usuarioAutenticadoService = usuarioAutenticadoService;
        }

        public async Task<IEnumerable<ConsultaAnaliseOutput>> ConsultarAnalises()
        {
            int? empresaId = null;

            if (_usuarioAutenticadoService.Usuario.UsuarioExterno)
            {
                empresaId = (await _empresaUsuarioRepository.ObterVinculoEmpresaUsuario(_usuarioAutenticadoService.Usuario.Id)).EmpresaId;
            }

            var analises = await _analiseRepository.ConsultarAnalises(empresaId);
            var output = analises.Select(analise => new ConsultaAnaliseOutput
            {
                Id = analise.Id,
                Titulo = analise.Titulo,
                Empresa = analise.Empresa.NomeFantasia,
                DataPublicacao = analise.DataPublicacao,
                DataUltimaEdicao = analise.DataAlteracao != null ? analise.DataAlteracao.Value : analise.DataInclusao,
                UsuarioUltimaModificacao = analise.DataAlteracao != null ? analise.UsuarioAlteracao : analise.UsuarioInclusao
            });

            return output;
        }
    }
}
