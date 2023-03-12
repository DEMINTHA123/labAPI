using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace labAPI.Entities
{
    public class Chemicals
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string MatterState { get; set; }
        public string Construction { get; set; }
    }
}
