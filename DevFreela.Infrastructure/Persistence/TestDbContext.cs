using Microsoft.EntityFrameworkCore;
using DevFreela.Infrastructure.Persistence;

namespace DevFreel.UnitTests.Infrastructure.Persistence
{
    public class TestDbContext : DevFreelaDbContext
    {
        public TestDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options)
        {
        }

    }
}
