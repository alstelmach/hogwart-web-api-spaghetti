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

        if (studentDto.IsAmbitious && studentDto.DoesSpeakParseltongue)
        {
            var slytherinHouse = houses.First(house => house.Name == "Slytherin");
            var hasEvenNumberOfStudents = slytherinHouse.Students.Count % 2 == 0;

            if (hasEvenNumberOfStudents)
            {
                slytherinHouse.Students.Add(studentDto);
                _sortingContext.Update(slytherinHouse);
            }
            else
            {
                var random = new Random();
                var randomHouseOrdinalNumber = random.Next(0, 3);
                var randomHouse = houses.ElementAt(randomHouseOrdinalNumber);
                randomHouse.Students.Add(studentDto);
                _sortingContext.Update(slytherinHouse);
            }
        }
        else if (studentDto.HeightInCentimeters > 180)
        {
            var gryffindorHouse = houses.First(house => house.Name == "Gryffindor");
            var hasEvenNumberOfStudents = gryffindorHouse.Students.Count % 2 == 0;

            if (hasEvenNumberOfStudents)
            {
                gryffindorHouse.Students.Add(studentDto);
                _sortingContext.Update(gryffindorHouse);
            }
            else
            {
                var random = new Random();
                var randomHouseOrdinalNumber = random.Next(0, 3);
                var randomHouse = houses.ElementAt(randomHouseOrdinalNumber);
                randomHouse.Students.Add(studentDto);
                _sortingContext.Update(gryffindorHouse);
            }
        }
        else if (studentDto.DoesPlayQuidditch)
        {
            var ravenclawHouse = houses.First(house => house.Name == "Ravenclaw");
            var hasEvenNumberOfStudents = ravenclawHouse.Students.Count % 2 == 0;

            if (hasEvenNumberOfStudents)
            {
                ravenclawHouse.Students.Add(studentDto);
                _sortingContext.Update(ravenclawHouse);
            }
            else
            {
                var random = new Random();
                var randomHouseOrdinalNumber = random.Next(0, 3);
                var randomHouse = houses.ElementAt(randomHouseOrdinalNumber);
                randomHouse.Students.Add(studentDto);
                _sortingContext.Update(ravenclawHouse);
            }
        }
        else
        {
            var hufflepuffHouse = houses.First(house => house.Name == "Hufflepuff");
            var hasEvenNumberOfStudents = hufflepuffHouse.Students.Count % 2 == 0;

            if (hasEvenNumberOfStudents)
            {
                hufflepuffHouse.Students.Add(studentDto);
                _sortingContext.Update(hufflepuffHouse);
            }
            else
            {
                var random = new Random();
                var randomHouseOrdinalNumber = random.Next(0, 3);
                var randomHouse = houses.ElementAt(randomHouseOrdinalNumber);
                randomHouse.Students.Add(studentDto);
                _sortingContext.Update(hufflepuffHouse);
            }
        }

        await _sortingContext.SaveChangesAsync(cancellationToken);

        return Created($"/students/{studentDto.Id}", studentDto);
    }
}
