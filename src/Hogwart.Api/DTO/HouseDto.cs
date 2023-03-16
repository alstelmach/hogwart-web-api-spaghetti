namespace Hogwart.Api.DTO;

public class HouseDto
{
    public string Name { get; init; }
    
    public ICollection<StudentDto> Students { get; init; }
}