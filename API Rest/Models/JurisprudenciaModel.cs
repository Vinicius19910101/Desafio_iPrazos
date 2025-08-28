namespace API_Rest.Models
{
    public class JurisprudenciaModel
    {
        public int Id { get; set; }
        public string NumeroProcesso { get; set; } = string.Empty;
        public string Classe { get; set; } = string.Empty;
        public string Assunto { get; set; } = string.Empty;
        public string Relator { get; set; } = string.Empty;
        public string OrgaoJulgador { get; set; } = string.Empty;
        public DateTime DataJulgamento { get; set; }
        public string Ementa { get; set; } = string.Empty;
    }
}
