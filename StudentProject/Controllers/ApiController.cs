using Microsoft.AspNetCore.Mvc;
using StudentProject.Data;
using StudentProject.Models; // Assuming this is the namespace for your data model

[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
    private readonly StudentDbContext _db;

    public ApiController(StudentDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAllStudents()
    {
        var students = _db.Students.ToList();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        var student = _db.Students.Find(id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    // Add more actions as needed for your API
}
