using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using mks.Data;
using mks.DTOs;
using mks.Interfaces;
using mks.Models;

namespace mks.Services
{
    public class CreditorService : ICreditorService
    {
        private readonly ApplicationDbContext _context;

        public CreditorService(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<ServiceResponse> CreateCreditorAsync(CreateCreditorDto dto)
        {
            var creditor= await _context.Creditor.AnyAsync(a=>a.name == dto.name || a.phone == dto.phone);

            if (creditor)
            return new ServiceResponse
            {
                Success = false,
                Message = "Creditor already exist"
            };
            var addCreditor = new Creditors
            {
                name= dto.name,
                phone= dto.phone,
                notes = dto.notes
            };

            _context.Creditor.Add(addCreditor);

            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                Success = true,
                Message = "Creditor created successfully",
                Response = addCreditor
            };
;        }
        public async Task<ServiceResponse> DeleteCreditorAsync(DeleteCreditorDto dto)
        {
            var creditor = await _context.Creditor.FirstOrDefaultAsync(a=>a.id == dto.id);

            if(creditor == null)
            return new ServiceResponse
            {
                Success=false,
                Message = "Creditor not fund"
            };
            _context.Creditor.Remove(creditor);

            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Success=true,
                Message ="Credito Deleted Successfully"
            };
        }

        public async Task<ServiceResponse> UpdateCreditorAsync(UpdateCreditorDto dto)
        {
            var creditor = await _context.Creditor.FirstOrDefaultAsync(a=>a.id == dto.id);

            if(creditor== null)
            return new ServiceResponse
            {
                Success=false,
                Message="Creditor not found"
            };

            if (!string.IsNullOrEmpty(dto.name))
            
                creditor.name = dto.name;
            
            if (!string.IsNullOrEmpty(dto.phone))
            
                creditor.phone= dto.phone;
            
            if (!string.IsNullOrEmpty(dto.notes))
            
                creditor.notes= dto.notes;
            

            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                Success=true,
                Message =" Creditor Updated successfully"
            };
        }

        public async Task<ServiceResponse> GetAllCreditorAsync()
        {
            var creditor = await _context.Creditor.ToListAsync();

            return new ServiceResponse
            {
                Success = true,
                Message ="Creditor retrived successfully",
                Response = creditor
            };
        }
    }
}