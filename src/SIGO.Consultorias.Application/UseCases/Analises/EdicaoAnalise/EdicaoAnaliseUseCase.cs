using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Application.Services;
using SIGO.Consultorias.Application.TransferObjects;
using SIGO.Consultorias.Entities;
using System;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.UseCases.Analises.EdicaoAnalise
{
    public class EdicaoAnaliseUseCase : IEdicaoAnaliseUseCase
    {
        private readonly IAnaliseRepository _analiseRepository;
        private readonly IEmpresaUsuarioRepository _empresaUsuarioRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;

        public EdicaoAnaliseUseCase(IAnaliseRepository analiseRepository, 
                                    IEmpresaUsuarioRepository empresaUsuarioRepository, 
                                    IUsuarioAutenticadoService usuarioAutenticadoService)
        {
            _analiseRepository = analiseRepository;
            _empresaUsuarioRepository = empresaUsuarioRepository;
            _usuarioAutenticado = usuarioAutenticadoService.Usuario;
        }

        public async Task<int> SalvarAnalise(EdicaoAnaliseInput analiseInput)
        {
            var analise = await _analiseRepository.ObterAnalisePorId(analiseInput.Id);
            
            if(analise == null)
            {
                var empresaUsuario = await _empresaUsuarioRepository.ObterVinculoEmpresaUsuario(_usuarioAutenticado.Id);

                analise = new Analise
                {
                    EmpresaId = empresaUsuario.EmpresaId,
                    UsuarioInclusaoId = _usuarioAutenticado.Id,
                    UsuarioInclusao = _usuarioAutenticado.Nome,
                    DataInclusao = DateTime.UtcNow
                };
            } 
            else
            {
                analise.UsuarioAlteracaoId = _usuarioAutenticado.Id;
                analise.UsuarioAlteracao = _usuarioAutenticado.Nome;
                analise.DataAlteracao = DateTime.UtcNow;
            }

            analise.Titulo = analiseInput.Titulo;
            analise.Resumo = analiseInput.Resumo;
            analise.DataPublicacao = analiseInput.DataPublicacao;

            if(analise.Id == 0)
            {
                await _analiseRepository.Inserir(analise);
            } 
            else
            {
                await _analiseRepository.Atualizar(analise);
            }

            return analise.Id;
        }
    }
}
