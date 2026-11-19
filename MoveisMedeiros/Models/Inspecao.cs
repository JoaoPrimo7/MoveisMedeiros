namespace MoveisMedeiros.Models
{
    public class Inspecao
    {
        public int Id { get; set; }

        public int ObraId { get; set; }

        public Obra? Obra { get; set; }

        public DateTime DataInspecao { get; set; } 

        public string Observacoes { get; set; } 

        public List<ItemInspecao> Itens { get; set; } 
    }
}
