using System.ComponentModel.DataAnnotations;

namespace labAPI.Entities
{
    public class Equipment
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
    }
}
