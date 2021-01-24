using SIGO.Consultorias.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SIGO.Consultorias.Application.Repositories
{
    public interface IAnaliseRepository
    {
        Task<Analise> ObterAnalisePorId(int id);
        Task Inserir(Analise analise);
        Task Atualizar(Analise analise);
        Task<IEnumerable<Analise>> ConsultarAnalises(int? empresaId);
    }
}
