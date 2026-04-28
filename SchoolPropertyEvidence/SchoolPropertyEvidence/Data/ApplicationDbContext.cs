using Microsoft.EntityFrameworkCore;
using SchoolPropertyEvidence.Models;

namespace SchoolPropertyEvidence.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<PeopleModel> Users { get; set; } = null!;
    }
}
