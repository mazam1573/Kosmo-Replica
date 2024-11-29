using EduConsultant.DTOs;
using EduConsultant.Interfaces.Repositories;
using EduConsultant.Interfaces.Services;
using EduConsultant.Models;

namespace EduConsultant.Services
{
    public class OfficeService: IOfficeService
    {
        private readonly IOfficeRepository _officeRepository;

        public OfficeService(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }

        public async Task<IEnumerable<OfficePostDTO>> GetOfficeListAsync()
        {
            var offices = await _officeRepository.GetAllAsync();
            return offices.Select(o => new OfficePostDTO
            {
                Id = o.Id,
                Name = o.Name,
                Location = o.Location,
                Address = o.Address,
                Phone = o.Phone,
                Timings = o.Timings,
                Manager = o.Manager != null ? new ReadShortUserDto
                {
                    Id = o.Manager.Id,
                    Name = o.Manager.First_Name + " " + o.Manager.Last_Name,
                    Email = o.Manager.Email,
                    Status = o.Manager.Status
                } : null
            });
        }

        public async Task<OfficePostDTO> GetOfficeByIdAsync(long id)
        {
            var office = await _officeRepository.GetByIdAsync(id);
            if (office == null) throw new Exception("Office not found");
            return MapOfficeToDTO(office);
        }

        private OfficePostDTO MapOfficeToDTO(Office office)
        {
            return new OfficePostDTO
            {
                Id = office.Id,
                Name = office.Name,
                Location = office.Location,
                Address = office.Address,
                Phone = office.Phone,
                Timings = office.Timings,
                Manager = new ReadShortUserDto
                {
                    Id = office.Manager.Id,
                    Name = office.Manager.First_Name + " " + office.Manager.Last_Name,
                    Email = office.Manager.Email,
                    Status = office.Manager.Status
                }
            };
        }


        public async Task CreateOfficeAsync(OfficePostDTO officeDto)
        {
            var office = new Office { Name = officeDto.Name, Location = officeDto.Location };
            await _officeRepository.AddAsync(office);
        }

    }
}
