namespace SIGO.Consultorias.Application.TransferObjects
{
    public class UsuarioAutenticado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Perfil { get; set; }
        public bool UsuarioExterno => Perfil == "Usuário Externo";
    }
}
