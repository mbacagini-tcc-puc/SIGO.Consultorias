using SIGO.Consultorias.Application.Services;
using SIGO.Consultorias.Application.TransferObjects;

namespace SIGO.Consultorias.API.Auth
{
    public class UsuarioAutenticadoService : IUsuarioAutenticadoService
    {
        public UsuarioAutenticado Usuario { get; set; }
    }
}
