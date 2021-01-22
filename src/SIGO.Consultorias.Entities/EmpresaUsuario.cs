namespace SIGO.Consultorias.Entities
{
    public class EmpresaUsuario : BaseEntity
    {
        public int UsuarioId { get; set; }
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
