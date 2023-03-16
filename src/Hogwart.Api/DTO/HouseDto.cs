using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hogwart.Api.DTO;

public class HouseDto
{
    [Key]
    public string Name { get; init; }
    
    [ForeignKey("houses_name_fkey")]
    public ICollection<StudentDto> Students { get; init; }
}