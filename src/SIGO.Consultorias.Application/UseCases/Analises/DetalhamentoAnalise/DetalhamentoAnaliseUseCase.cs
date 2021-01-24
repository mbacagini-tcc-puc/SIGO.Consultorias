using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Application.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.UseCases.Analises.DetalhamentoAnalise
{
    public class DetalhamentoAnaliseUseCase : IDetalhamentoAnaliseUseCase
    {
        private readonly IAnaliseRepository _analiseRepository;
        private readonly IEmpresaUsuarioRepository _empresaUsuarioRepository;
        private readonly IUsuarioAutenticadoService _usuarioAutenticadoService;

        public DetalhamentoAnaliseUseCase(IAnaliseRepository analiseRepository, IUsuarioAutenticadoService usuarioAutenticadoService, IEmpresaUsuarioRepository empresaUsuarioRepository)
        {
            _analiseRepository = analiseRepository;
            _usuarioAutenticadoService = usuarioAutenticadoService;
            _empresaUsuarioRepository = empresaUsuarioRepository;
        }

        public async Task<DetalhamentoAnaliseOutput> ObterDetalhesAnalise(int analiseId)
        {
            var analise = await _analiseRepository.ObterAnaliseDetalhada(analiseId);

            if (analise == null)
            {
                return null;
            }

            if (_usuarioAutenticadoService.Usuario.UsuarioExterno)
            {
                var empresaUsuario = await _empresaUsuarioRepository.ObterVinculoEmpresaUsuario(_usuarioAutenticadoService.Usuario.Id);

                if (empresaUsuario.EmpresaId != analise.EmpresaId)
                {
                    return null;
                }
            }

            var output = new DetalhamentoAnaliseOutput
            {
                Id = analise.Id,
                Titulo = analise.Titulo,
                Resumo = analise.Resumo,
                Empresa = analise.Empresa.NomeFantasia,
                DataInclusao = analise.DataInclusao,
                DataAlteracao = analise.DataAlteracao,
                DataPublicacao = analise.DataPublicacao,
                UsuarioInclusao = analise.UsuarioInclusao,
                UsuarioAlteracao = analise.UsuarioAlteracao,
                Anexos = analise.Anexos.Select(anexo => new AnexoOutput
                {
                    Id = anexo.Id,
                    NomeArquivo = anexo.NomeArquivo,
                    DataInclusao = anexo.DataInclusao
                })
            };

            return output;
        }
    }
}
