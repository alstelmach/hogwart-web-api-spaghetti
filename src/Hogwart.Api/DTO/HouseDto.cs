using System.ComponentModel.DataAnnotations;

namespace Hogwart.Api.DTO;

public class HouseDto
{
    [Key]
    public string Name { get; init; }

    public ICollection<StudentDto> Students { get; init; }
}
