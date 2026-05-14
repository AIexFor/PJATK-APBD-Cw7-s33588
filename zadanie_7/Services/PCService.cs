using Microsoft.EntityFrameworkCore;

using zadanie_7.DTOs;
using zadanie_7.Models;
using zadanie_7.Controllers;
using zadanie_7.Data;

namespace zadanie_7.Services;

public class PCService : IPCService
{
    private readonly AppDbContext _context;

    public PCService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PCResponseDto>> GetAllPCsAsync()
    {
        return await _context.PCs
            .Select(pc => new PCResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            }).ToListAsync();
    }

    public async Task<PCDetailsResponseDto?> GetPCDetailsAsync(int id)
    {
        var pc = await _context.PCs
            .Include(p => p.PCComponents)
                .ThenInclude(pc => pc.Component)
                    .ThenInclude(c => c.ComponentManufacturer)
            .Include(p => p.PCComponents)
                .ThenInclude(pc => pc.Component)
                    .ThenInclude(c => c.ComponentType)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pc == null) return null;

        return new PCDetailsResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock,
            Components = pc.PCComponents.Select(c => new PCComponentDetailDto
            {
                Amount = c.Amount,
                Component = new ComponentInfoDto
                {
                    Code = c.Component.Code,
                    Name = c.Component.Name,
                    Description = c.Component.Description,
                    Manufacturer = new ManufacturerDto
                    {
                        Id = c.Component.ComponentManufacturer.Id,
                        Abbreviation = c.Component.ComponentManufacturer.Abbreviation,
                        FullName = c.Component.ComponentManufacturer.FullName,
                        FoundationDate = c.Component.ComponentManufacturer.FoundationDate
                    },
                    Type = new TypeDto
                    {
                        Id = c.Component.ComponentType.Id,
                        Abbreviation = c.Component.ComponentType.Abbreviation,
                        Name = c.Component.ComponentType.Name
                    }
                }
            }).ToList()
        };
    }

    public async Task<PCResponseDto> CreatePCAsync(PCRequestDto dto)
    {
        var newPc = new PC
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };

        _context.PCs.Add(newPc);
        await _context.SaveChangesAsync();

        return new PCResponseDto
        {
            Id = newPc.Id,
            Name = newPc.Name,
            Weight = newPc.Weight,
            Warranty = newPc.Warranty,
            CreatedAt = newPc.CreatedAt,
            Stock = newPc.Stock
        };
    }

    public async Task<bool> UpdatePCAsync(int id, PCRequestDto dto)
    {
        var pc = await _context.PCs.FindAsync(id);
        if (pc == null) return false;

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePCAsync(int id)
    {
        var pc = await _context.PCs.FindAsync(id);
        if (pc == null) return false;

        _context.PCs.Remove(pc);
        await _context.SaveChangesAsync();
        return true;
    }
}