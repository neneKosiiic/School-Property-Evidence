using Microsoft.EntityFrameworkCore;
using SchoolPropertyEvidence.Models;

namespace SchoolPropertyEvidence.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<PeopleModel> Users { get; set; }
        public DbSet<PeopleModel> People { get; set; }
        public DbSet<RoomModel> Rooms { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ItemModel> Items { get; set; }
    }
}
