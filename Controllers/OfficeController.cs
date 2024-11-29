using EduConsultant.Data;
using EduConsultant.DTOs;
using EduConsultant.Interfaces.Services;
using EduConsultant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


namespace EduConsultant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : Controller
    {
        private readonly EduConsultantContext _context;
        private readonly IOfficeService _officeService;

        public OfficeController(IOfficeService officeService, EduConsultantContext context)
        {
            _officeService = officeService;
            _context = context;
        }

        //public OfficeController(EduConsultantContext context)
        //{
        //    _context = context;
        //}

        // GET: api/office
        [Authorize]
        [HttpGet("officeList")]
        public async Task<ActionResult<IEnumerable<OfficePostDTO>>> GetOffices()
        {
            var offices = await _officeService.GetOfficeListAsync();
            return Ok(offices);
            //return await _context.Office.Select(o => new OfficePostDTO
            //{
            //    Id = o.Id,
            //    Name = o.Name,
            //    Location = o.Location,
            //    Address = o.Address,
            //    Phone = o.Phone,
            //    Timings = o.Timings,
            //    ManagerId = o.Manager.Id,
            //    Manager = new ReadShortUserDto
            //    {
            //        Id = o.Manager.Id,
            //        Name = o.Manager.First_Name + ' ' + o.Manager.Last_Name,
            //        Email = o.Manager.Email,
            //        Status = o.Manager.Status
            //    }
            //}).ToListAsync();
        }

        // GET: api/office/5
        [Authorize]
        [HttpGet("getOfficeByID/{id}")]
        public async Task<ActionResult<OfficePostDTO>> GetOffice(long id)
        {
            var newOffice = await _context.Office
                .Where(o => o.Id == id)
                .Select(o => new OfficePostDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    Location = o.Location,
                    Address = o.Address,
                    Phone = o.Phone,
                    Timings = o.Timings,
                    ManagerId = o.Manager.Id,
                    Manager = new ReadShortUserDto
                    {
                        Id = o.Manager.Id,
                        Name = o.Manager.First_Name + ' ' + o.Manager.Last_Name,
                        Email = o.Manager.Email,
                        Status = o.Manager.Status
                    }
                }).FirstOrDefaultAsync();

            if (newOffice == null) return NotFound();

            return newOffice;
        }
        // POST: api/office
        [Authorize]
        [HttpPost("saveOffice")]
        public async Task<ActionResult<OfficePostDTO>> CreateOffice(OfficeDTO officeDto)
        {
            var manager = await _context.User.FirstOrDefaultAsync(u => u.Id == officeDto.ManagerId);

            if (manager == null) return BadRequest("Invalid manager ID.");

            var office = new Office
            {
                Name = officeDto.Name,
                Location = officeDto.Location,
                Address = officeDto.Address,
                Phone = officeDto.Phone,
                Timings = officeDto.Timings,
                ManagerId = officeDto.ManagerId
            };

            _context.Office.Add(office);
            await _context.SaveChangesAsync();

            var newOffice = await _context.Office
                .Where(o => o.Id == office.Id)
                .Select(o => new OfficePostDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    Location = o.Location,
                    Address = o.Address,
                    Phone = o.Phone,
                    Timings = o.Timings,
                    Manager = new ReadShortUserDto // UserDTO with limited properties to avoid cycle
                    {
                        Id = o.Manager.Id,
                        Name = o.Manager.First_Name + ' ' + o.Manager.Last_Name,
                        Email = o.Manager.Email,
                        Status = o.Manager.Status
                        // Add only necessary fields
                    }
                }).FirstOrDefaultAsync();

            return newOffice;
        }

        // GET: api/EditOffice
        [Authorize]
        [HttpGet("editOffice/{id}")]
        public async Task<ActionResult<ReadOfficeDTO>> EditOffice(long id)
        {
            var newOffice = await _context.Office
                .Where(o => o.Id == id)
                .Select(o => new OfficeDTO
                {
                    Name = o.Name,
                    Location = o.Location,
                    Address = o.Address,
                    Phone = o.Phone,
                    Timings = o.Timings,
                }).FirstOrDefaultAsync();
            if (newOffice == null)
                return NotFound();

            var admins = await _context.User.Select(u => new ReadShortUserDto
            {
                Id = u.Id,
                Name = u.First_Name+' '+u.Last_Name,
                Email = u.Email,
                Status = u.Status,
            }).ToListAsync();


            var data = new ReadOfficeDTO
            {
                Office = newOffice,
                Admins = admins,
            };

            return data;
        }

        // PUT: api/office/5
        [Authorize]
        [HttpPut("updateOffice/{id}")]
        public async Task<IActionResult> UpdateOffice(long id, OfficeDTO officeDto)
        {
            var office = await _context.Office.FindAsync(id);

            if (office == null) return NotFound();

            office.Name = officeDto.Name;
            office.Location = officeDto.Location;
            office.Address = officeDto.Address;
            office.Phone = officeDto.Phone;
            office.Timings = officeDto.Timings;
            office.ManagerId = officeDto.ManagerId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/office/5
        [Authorize]
        [HttpDelete("deleteOfficeByID/{id}")]
        public async Task<IActionResult> DeleteOffice(long id)
        {
            var office = await _context.Office.FindAsync(id);

            if (office == null) return NotFound();

            _context.Office.Remove(office);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
