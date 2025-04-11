using System;
using System.Collections.Generic;

namespace DragonBall.Domain.Entities
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty; 
        public string Race { get; set; } = string.Empty; 
        public string Ki { get; set; } = string.Empty; 
        public string Gender { get; set; } = string.Empty; 
        public string Affiliation { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 
        public List<Transformation> Transformations { get; set; } = new List<Transformation>(); 

    }

}
