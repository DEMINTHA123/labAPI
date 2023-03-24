using labAPI.Entities;

namespace labAPI.DTOs.EquipmentDTO
{
    public class EquipmentInputDTO
    {
        public string? Name { get; set; }
        public int Qty { get; set; }
        public string? Description { get; set; }
    }
}
