namespace MoveisMedeiros.Models
{
    public class Obra
    {
        public int Id { get; set; }

        public string NomeCliente { get; set; } 

        public string NomeObra { get; set; } 

        public string Local { get; set; }
        public DateTime PrazoEntrega { get; set; }

        public DateTime CriadaEm { get; set; } 

        public List<Inspecao> Inspecoes { get; set; } 
    }
}
