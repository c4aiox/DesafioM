using Microsoft.AspNetCore.Mvc;
using BikeRental.Api.Models;
using BikeRental.Api.Data;
using Microsoft.Extensions.Logging;


namespace BikeRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MotorcycleController : ControllerBase
{

    private readonly AppDbContext _context;
    private readonly ILogger<MotorcycleController> _logger;
    public MotorcycleController(AppDbContext context, ILogger<MotorcycleController> logger)
    {
        _context = context;
        _logger = logger;
    }


    [HttpGet("")]
    public IActionResult GetMotorcycles()
    {
        var motorcycles = _context.Motorcycles.ToList();
        if (motorcycles == null || !motorcycles.Any())
        {
            return NotFound("No motorcycles found.");
        }
        return Ok(motorcycles);
    }

    [HttpGet("plate/{plate}")]
    public IActionResult GetMotorcycleByPlate(string plate)
    {
        var motorcycle = _context.Motorcycles.FirstOrDefault(m => m.Plate == plate);
        if (motorcycle == null)
        {
            return NotFound($"Motorcycle with plate {plate} not found.");
        }
        return Ok(motorcycle);
    }

    [HttpGet("{id}")]

    public IActionResult GetMotorcycleById(string id)
    {
        var motorcycle = _context.Motorcycles.Find(id);
        if (motorcycle == null)
        {
            return NotFound();
        }
        return Ok(motorcycle);
    }

    [HttpPost("")]
    public IActionResult CreateMotorcycle([FromBody] Motorcycle motorcycle)
    {
        if (motorcycle.Plate == null) //validation placa = null
        {
            return BadRequest("Plate is required.");
        }

        if (motorcycle.Model == null) //validation model = null
        {
            return BadRequest("Model is required.");
        }

        if (motorcycle.Id == null) // validation id = null
        {
            return BadRequest("Id is required.");
        }

        _logger.LogInformation($"Motorcycle created: {motorcycle.Model}, Plate: {motorcycle.Plate}");
        _context.Motorcycles.Add(motorcycle);
        _context.SaveChanges();

        return Ok();
    
    }
}   