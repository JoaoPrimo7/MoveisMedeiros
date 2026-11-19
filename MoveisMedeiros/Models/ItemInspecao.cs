namespace MoveisMedeiros.Models
{
    public class ItemInspecao
    {
        public int Id { get; set; }

        public int InspecaoId { get; set; }

        public Inspecao? Inspecao { get; set; }

        public string Categoria { get; set; } 

        public string Status { get; set; } 
    }
}
