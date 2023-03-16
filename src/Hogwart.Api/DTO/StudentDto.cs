using Microsoft.EntityFrameworkCore;

namespace Hogwart.Api.DTO;

[Keyless]
public class StudentDto
{
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
    
    public string Nationality { get; init; }
    
    public int Age { get; init; }
    
    public float HeightInCentimeters { get; init; }
    
    public bool DoesSpeakParseltongue { get; init; }
    
    public bool DoesPlayQuidditch { get; init; }
    
    public bool IsAmbitious { get; init; }
}
