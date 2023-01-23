using System.ComponentModel.DataAnnotations;

namespace labAPI.Entities
{
    public class Lab
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
    }
}
