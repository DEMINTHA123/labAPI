using System.ComponentModel.DataAnnotations;

namespace labAPI.Entities
{
    public class Reactions
    {
        [Key]
        public string Id { get; set; }
        public string Structure { get; set; }
    }
}
