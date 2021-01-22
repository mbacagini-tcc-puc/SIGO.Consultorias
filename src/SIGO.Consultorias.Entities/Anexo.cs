namespace SIGO.Consultorias.Entities
{
    public class Anexo : BaseEntity
    {
        public int AnaliseId { get; set; }
        public string NomeArquivo { get; set; }
        public string Caminho { get; set; }
        public virtual Analise Analise { get; set; }
    }
}
