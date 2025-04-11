using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DragonBall.Domain.Entities
{
    public class Transformation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Power { get; set; } = string.Empty;

        [ForeignKey("Character")]
        public int CharacterId { get; set; }

        [JsonIgnore]
        public Character? Character { get; set; }
    }
}
