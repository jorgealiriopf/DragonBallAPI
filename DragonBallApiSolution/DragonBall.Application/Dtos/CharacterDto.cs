namespace DragonBall.Application.Dtos

{
    public class CharacterDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Ki { get; set; }
        public required string Race { get; set; }
        public required string Gender { get; set; }
        public required string Affiliation { get; set; }
        public required string Description { get; set; }

        // Si aún no estás trabajando con transformaciones, puedes quitar esta línea.
        public List<TransformationDto>? Transformations { get; set; }
    }

}
