using labAPI.Entities;

namespace labAPI.DTOs.EquipmentDTO
{
    public class EquipmentInputDTO
    {
       // public string? Id { get; set; }
        public string? Name { get; set; }
        public int Qty { get; set; }

        //public IFormFile Photo { get; set; }
        public string? Description { get; set; }
    }
}
