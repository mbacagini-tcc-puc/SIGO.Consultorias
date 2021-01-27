using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Application.Services;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.UseCases.Analises.DownloadAnexo
{
    public class DownloadAnexoUseCase : IDownloadAnexoUseCase
    {
        private readonly IAnexoRepository _anexoRepository;
        private readonly IFileService _fileService;
        private readonly IEmpresaUsuarioRepository _empresaUsuarioRepository;
        private readonly IUsuarioAutenticadoService _usuarioAutenticadoService;

        public DownloadAnexoUseCase(IAnexoRepository anexoRepository, IFileService fileService, IEmpresaUsuarioRepository empresaUsuarioRepository, IUsuarioAutenticadoService usuarioAutenticadoService)
        {
            _anexoRepository = anexoRepository;
            _fileService = fileService;
            _empresaUsuarioRepository = empresaUsuarioRepository;
            _usuarioAutenticadoService = usuarioAutenticadoService;
        }

        public async Task<string> ObterDownloadLink(int anexoId)
        {
            var anexo = await _anexoRepository.ObterPorId(anexoId);

            if (_usuarioAutenticadoService.Usuario.UsuarioExterno)
            {
                var empresaUsuario = await _empresaUsuarioRepository.ObterVinculoEmpresaUsuario(_usuarioAutenticadoService.Usuario.Id);

                if (empresaUsuario.EmpresaId != anexo.Analise.EmpresaId)
                {
                    return null;
                }
            }

            var urlDownload = await _fileService.ObterDownloadLink(anexo.Caminho);

            return urlDownload;
        }
    }
}
