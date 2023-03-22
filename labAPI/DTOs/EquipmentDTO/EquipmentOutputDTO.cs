using labAPI.Entities;

namespace labAPI.DTOs.EquipmentDTO
{
    public class EquipmentOutputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }

        public int Photo { get; set; }
        public string Description { get; set; }
    }
}
