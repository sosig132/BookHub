using Back_End.Models.BookDetails;
using Back_End.Models.Books;
using Back_End.Models.Categories;
using Back_End.Models.Many_To_Many;
using Back_End.Models.Reviews;
using Back_End.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Data
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookDetails> BookDetails { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One-To-One
            modelBuilder.Entity<Book>()
            .HasOne(b => b.BookDetails)
            .WithOne(bd => bd.Book)
            .HasForeignKey<BookDetails>(bd => bd.BookId);


            //One-To-Many
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Reviews)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            //Many-To-Many
            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);

            modelBuilder.Entity<User>().HasData(new User {Id = Guid.NewGuid(), Username = "admin", Password = BCrypt.Net.BCrypt.HashPassword("admin"), Role = Enums.Role.Admin, DisplayName = "admin", Email = "sosig132@gmail.com" });

            base.OnModelCreating(modelBuilder);
        }

    }
}
