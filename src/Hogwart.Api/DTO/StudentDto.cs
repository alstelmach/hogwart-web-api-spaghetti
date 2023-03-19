using System.ComponentModel.DataAnnotations;

namespace Hogwart.Api.DTO;

public class StudentDto
{
    [Key]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public int Id { get; init; }

    public string FirstName { get; init; }
    
    public string LastName { get; init; }
    
    public string Nationality { get; init; }
    
    public int Age { get; init; }
    
    public float HeightInCentimeters { get; init; }
    
    public bool DoesSpeakParseltongue { get; init; }
    
    public bool DoesPlayQuidditch { get; init; }
    
    public bool IsAmbitious { get; init; }
}
