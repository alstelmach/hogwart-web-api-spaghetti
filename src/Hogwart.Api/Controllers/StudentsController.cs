using Hogwart.Api.DTO;
using Hogwart.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hogwart.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly SortingContext _sortingContext;

    public StudentsController(SortingContext sortingContext)
    {
        _sortingContext = sortingContext;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(
        [FromRoute] int id,
        CancellationToken cancellationToken = default)
    {
        var student = await _sortingContext
            .Students
            .FirstOrDefaultAsync(
                student =>
                    student.Id == id,
                cancellationToken);

        return student is null
            ? NotFound()
            : Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] StudentDto studentDto,
        CancellationToken cancellationToken = default)
    {
        if (studentDto is null)
        {
            return BadRequest();
        }
        
        if (studentDto is { Age: > 18 or < 11 })
        {
            return BadRequest("You're too young or too old to study in Hogwart");
        }

        var houses = await _sortingContext
            .Houses
            .Include(house => house.Students)
            .ToListAsync(cancellationToken);

        if (CanAssignToSlytherin(studentDto))
        {
            AssignStudentToHouse(
                studentDto,
                houses,
                "Slytherin");
        }
        else if (CanAssignToGryffindor(studentDto))
        {
            AssignStudentToHouse(
                studentDto,
                houses,
                "Gryffindor");
        }
        else if (CanAssignToRavenclaw(studentDto))
        {
            AssignStudentToHouse(
                studentDto,
                houses,
                "Ravenclaw");
        }
        else
        {
            AssignStudentToHouse(
                studentDto,
                houses,
                "Hufflepuff");
        }

        await _sortingContext.SaveChangesAsync(cancellationToken);

        return Created($"/students/{studentDto.Id}", studentDto);
    }

    [HttpPatch("{studentId:int}")]
    public async Task<IActionResult> PatchAsync(
        [FromRoute] int studentId,
        [FromBody] PatchStudentHouseDto houseDto,
        CancellationToken cancellationToken = default)
    {
        if (houseDto is null || studentId == default)
        {
            return BadRequest();
        }

        var student = await _sortingContext
            .Students
            .FirstOrDefaultAsync(
                student =>
                    student.Id == studentId,
                cancellationToken);

        if (student is null)
        {
            return NotFound();
        }

        var isHouseInvalid = houseDto is not { HouseName: "Gryffindor" or "Hufflepuff" or "Ravenclaw" or "Slytherin" };

        if (isHouseInvalid)
        {
            return BadRequest();
        }

        var actualHouse = await _sortingContext
            .Houses
            .Include(house => house.Students)
            .FirstOrDefaultAsync(
                house =>
                    house.Students.Any(assignedStudent =>
                        assignedStudent.Id == studentId),
                cancellationToken);

        if (actualHouse is null)
        {
            return BadRequest();
        }

        var expectedHouse = await _sortingContext
            .Houses
            .Include(house => house.Students)
            .FirstOrDefaultAsync(
                house =>
                    house.Name == houseDto.HouseName,
                cancellationToken);

        actualHouse.Students.Remove(student);
        expectedHouse.Students.Add(student);

        _sortingContext.Houses.Update(actualHouse);
        _sortingContext.Houses.Update(expectedHouse);

        await _sortingContext.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    private void AssignStudentToHouse(
        StudentDto studentDto,
        IReadOnlyCollection<HouseDto> houses,
        string houseName)
    {
        var firstChosenHouse = houses.First(house => house.Name == houseName);
        var hasEvenNumberOfStudents = firstChosenHouse.Students.Count % 2 == 0;

        if (hasEvenNumberOfStudents)
        {
            firstChosenHouse.Students.Add(studentDto);
            _sortingContext.Update(firstChosenHouse);
        }
        else
        {
            RandomizeHouse(studentDto, houses);
        }
    }

    private void RandomizeHouse(
        StudentDto studentDto,
        IReadOnlyCollection<HouseDto> houses)
    {
        var random = new Random();
        var randomHouseOrdinalNumber = random.Next(0, 3);
        var randomHouse = houses.ElementAt(randomHouseOrdinalNumber);
        var hasFoundHouse = false;

        switch (randomHouse.Name)
        {
            case "Slytherin" when CanAssignToSlytherin(studentDto):
                randomHouse.Students.Add(studentDto);
                hasFoundHouse = true;
                break;
            case "Gryffindor" when CanAssignToGryffindor(studentDto):
                randomHouse.Students.Add(studentDto);
                hasFoundHouse = true;
                break;
            case "Ravenclaw" when CanAssignToRavenclaw(studentDto):
                randomHouse.Students.Add(studentDto);
                hasFoundHouse = true;
                break;
            case "Hufflepuff":
                randomHouse.Students.Add(studentDto);
                hasFoundHouse = true;
                break;
        }

        if (hasFoundHouse)
        {
            randomHouse.Students.Add(studentDto);
            _sortingContext.Update(randomHouse);
            return;
        }
        
        RandomizeHouse(
            studentDto,
            houses);
    }

    private static bool CanAssignToSlytherin(StudentDto studentDto) =>
        studentDto.IsAmbitious && studentDto.DoesSpeakParseltongue;
    
    private static bool CanAssignToGryffindor(StudentDto studentDto) =>
        studentDto.HeightInCentimeters > 180;
    
    private static bool CanAssignToRavenclaw(StudentDto studentDto) =>
        studentDto.DoesPlayQuidditch;
}
