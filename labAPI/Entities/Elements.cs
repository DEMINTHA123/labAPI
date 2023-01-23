using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace labAPI.Entities
{
    public class Elements
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int MolarMass { get; set; }
    }
}
