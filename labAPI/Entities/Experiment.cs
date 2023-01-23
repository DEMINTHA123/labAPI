using System.ComponentModel.DataAnnotations;

namespace labAPI.Entities
{
    public class Experiment
    {
        [Key]
        public string Id { get; set; }
        public string Pro { get; set; }
    }
}
