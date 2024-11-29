using EduConsultant.Data;
using EduConsultant.DTOs;
using EduConsultant.Interfaces.Repositories;
using EduConsultant.Models;
using System.Data.Entity;

namespace EduConsultant.Repositories
{
    public class OfficeRepository : Repository<Office>, IOfficeRepository
    {
        public OfficeRepository(EduConsultantContext context) : base(context) { }

        public async Task<IEnumerable<OfficePostDTO>> GetOfficeListAsync()
        {
            return (IEnumerable<OfficePostDTO>)await _dbSet.ToListAsync();
        }
    }
}
