namespace MoveisMedeiros.Models
{
    public class Inspecao
    {
        public int Id { get; set; }
        public int ObraId { get; set; }
        public Obra? Obra { get; set; }
        public DateTime DataInspecao { get; set; } = DateTime.Now;
        public string Montagem { get; set; } = "Conforme";
        public string Acabamento { get; set; } = "Conforme";
        public string Ferragens { get; set; } = "Conforme";
        public string Limpeza { get; set; } = "Conforme";

        public string? Observacoes { get; set; }
    }
}