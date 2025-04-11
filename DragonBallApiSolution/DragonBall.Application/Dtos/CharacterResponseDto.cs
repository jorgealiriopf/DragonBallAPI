using Microsoft.CodeAnalysis;

namespace DragonBall.Application.Dtos

{

    public class CharacterResponseDto
    {
        public required List<CharacterDto> Items { get; set; }
        public MetaDto? Meta { get; set; }
        public LinkDto? Links { get; set; }
    }
}
