using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MixSubmit.Models;

namespace MixSubmit.Controllers;

[ApiController]
[Route("api/artists")]
public class SetController : ControllerBase
{
    private readonly SetContext _setContext;

    public SetController(SetContext setContext)
    {
        _setContext = setContext;
    }

    private static string GenerateUniqueCode(ICollection<string> codes)
    {
        // Generate a random five letter string code and make sure it doesn't already exist
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        string code;
        var random = new Random();
        
        // I think this might be my first ever do while loop?
        do
        {
            code = new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        } while (codes.Contains(code));

        return code;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateSet(string artist, SetType type)
    {
        // Get a list of all set codes to avoid collisions
        var codes = await _setContext.Sets.Select(set => set.Code).ToListAsync();
        var code = GenerateUniqueCode(codes);
        
        // Create a new set with the code and artist name
        var set = new Set {Artist = artist, Code = code, Status = SetStatus.InProgress, SetType = type};
        await _setContext.Sets.AddAsync(set);
        await _setContext.SaveChangesAsync();
        
        return Ok(set);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetSet([StringLength(5, MinimumLength = 5)] string code)
    {
        var set = await _setContext.Sets.FirstOrDefaultAsync(s => s.Code == code);
        
        if (set == null)
        {
            return NotFound();
        }
        
        return Ok(set);
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> DeleteSet([StringLength(5, MinimumLength = 5)] string code)
    {
        var set = await _setContext.Sets.FirstOrDefaultAsync(s => s.Code == code);
        
        if (set == null)
        {
            return NotFound();
        }
        
        _setContext.Sets.Remove(set);
        await _setContext.SaveChangesAsync();
        
        return Ok();
    }
    
    [HttpPut("{code}")]
    public async Task<IActionResult> UpdateSetStatus([StringLength(5, MinimumLength = 5)] string code, SetStatus status)
    {
        var set = await _setContext.Sets.FirstOrDefaultAsync(s => s.Code == code);
        
        if (set == null)
        {
            return NotFound();
        }
        
        set.Status = status;
        await _setContext.SaveChangesAsync();
        
        return Ok(set);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSets()
    {
        var sets = await _setContext.Sets.ToListAsync();
        return Ok(sets);
    }
}