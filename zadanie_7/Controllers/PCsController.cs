using Microsoft.AspNetCore.Mvc;
using zadanie_7.DTOs;
using zadanie_7.Services;

namespace zadanie_7.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PCsController : ControllerBase
{
    private readonly IPCService _pcService;

    public PCsController(IPCService pcService)
    {
        _pcService = pcService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPCs()
    {
        var pcs = await _pcService.GetAllPCsAsync();
        return Ok(pcs);
    }

    [HttpGet("{id}/components")]
    public async Task<IActionResult> GetPCComponents(int id)
    {
        var result = await _pcService.GetPCDetailsAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePC([FromBody] PCRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var createdPc = await _pcService.CreatePCAsync(request);
        return Created($"/api/pcs/{createdPc.Id}", createdPc);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePC(int id, [FromBody] PCRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updated = await _pcService.UpdatePCAsync(id, request);
        if (!updated)
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePC(int id)
    {
        var deleted = await _pcService.DeletePCAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}