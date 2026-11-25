namespace MoveisMedeiros.Models
{
    public class Obra
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string NomeObra { get; set; } = string.Empty;
        public string LocalObra { get; set; } = string.Empty;
        public DateTime PrazoEntrega { get; set; }
        public List<Inspecao> Inspecoes { get; set; } = new();
    }
}