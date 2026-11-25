namespace MoveisMedeiros.DTOs
{
    public class InspecaoDto
    {
        public int ObraId { get; set; }
        public string Montagem { get; set; } = "Conforme";
        public string Acabamento { get; set; } = "Conforme";
        public string Ferragens { get; set; } = "Conforme";
        public string Limpeza { get; set; } = "Conforme";
        public string? Observacoes { get; set; }
    }
}