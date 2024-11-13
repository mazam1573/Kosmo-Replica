using EduConsultant.Models;
using Microsoft.EntityFrameworkCore;

namespace EduConsultant.Data
{
    public class EduConsultantContext: DbContext
    {
        public EduConsultantContext(DbContextOptions<EduConsultantContext> options): base(options) { }

        public DbSet<User> User { get; set; } = null!;
        public DbSet<Office> Office { get; set; } = null!;

    }
}
