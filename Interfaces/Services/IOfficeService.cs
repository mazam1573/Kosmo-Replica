using EduConsultant.DTOs;

namespace EduConsultant.Interfaces.Services
{
    public interface IOfficeService
    {
        Task<IEnumerable<OfficePostDTO>> GetOfficeListAsync();
        Task<OfficePostDTO> GetOfficeByIdAsync(long id);
        Task CreateOfficeAsync(OfficePostDTO officeDto);
    }

}
