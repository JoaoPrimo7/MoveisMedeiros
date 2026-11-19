using MoveisMedeiros.DTOs;

namespace MoveisMedeiros.Api.DTOs;

public class InspecaoDto
{
    public int ObraId { get; set; }
    public string Observacoes { get; set; }
    public List<ItemInspecaoDto> Itens { get; set; }
}