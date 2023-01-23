using System.ComponentModel.DataAnnotations;

namespace labAPI.Entities
{
    public class Chemicals
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string MatterState { get; set; }
        public string Construction { get; set; }
    }
}
