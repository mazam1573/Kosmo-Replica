using EduConsultant.DTOs;
using EduConsultant.Models;

namespace EduConsultant.Interfaces.Repositories
{
    public interface IOfficeRepository : IRepository<Office>
    {
        Task<IEnumerable<OfficePostDTO>> GetOfficeListAsync();
    }

}
