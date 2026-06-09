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

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PeopleModel>(entity => {
                entity.ToTable("people");
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<CategoryModel>(entity => {
                entity.ToTable("categories");
                entity.HasKey(c => c.Id);
            });

            modelBuilder.Entity<RoomModel>(entity => {
                entity.ToTable("rooms");
                entity.HasKey(r => r.Id);

                entity.HasOne(r => r.ResponsiblePerson)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(r => r.ResponsiblePersonId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            modelBuilder.Entity<ItemModel>(entity => {
                entity.ToTable("items");
                entity.HasKey(i => i.Id);

                entity.HasOne(i => i.Category)
                    .WithMany(c => c.Items)
                    .HasForeignKey(i => i.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                entity.HasOne(i => i.Room)
                    .WithMany(r => r.Items)
                    .HasForeignKey(i => i.RoomId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });
        }
    }
}
