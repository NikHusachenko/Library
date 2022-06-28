using Library.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ManagerEntity> Managers { get; set; }
        public DbSet<VisitorEntity> Visitors { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<HistoryEntity> Histories { get; set; }

        public ApplicationDbContext() : base()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=LibraryDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ManagerEntity>().Property(manager => manager.Login).HasMaxLength(15);
            modelBuilder.Entity<ManagerEntity>().Property(manager => manager.Name).HasMaxLength(15);
            modelBuilder.Entity<ManagerEntity>().Property(manager => manager.Password).HasMaxLength(31);
            modelBuilder.Entity<ManagerEntity>().Property(manager => manager.Surname).HasMaxLength(15);

            modelBuilder.Entity<VisitorEntity>().Property(visitor => visitor.Login).HasMaxLength(15);
            modelBuilder.Entity<VisitorEntity>().Property(visitor => visitor.Name).HasMaxLength(15);
            modelBuilder.Entity<VisitorEntity>().Property(visitor => visitor.Password).HasMaxLength(31);
            modelBuilder.Entity<VisitorEntity>().Property(visitor => visitor.Surname).HasMaxLength(15);

            modelBuilder.Entity<BookEntity>().Property(book => book.Author).HasMaxLength(31);
            modelBuilder.Entity<BookEntity>().Property(book => book.Title).HasMaxLength(31);

            modelBuilder.Entity<VisitorEntity>()
                .HasMany<BookEntity>(visitor => visitor.Books)
                .WithOne(book => book.Visitor)
                .HasForeignKey(book => book.VisitorFK);
        }
    }
}