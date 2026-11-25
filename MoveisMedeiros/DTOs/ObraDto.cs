namespace MoveisMedeiros.DTOs
{
    public class ObraDto
    {
        public string NomeCliente { get; set; } = string.Empty;
        public string NomeObra { get; set; } = string.Empty;
        public string LocalObra { get; set; } = string.Empty;
        public DateTime PrazoEntrega { get; set; }
    }
}