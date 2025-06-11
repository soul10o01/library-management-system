using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.Book)
                .WithMany()
                .HasForeignKey(bl => bl.BookId);

            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.Member)
                .WithMany(m => m.BookLoans)
                .HasForeignKey(bl => bl.MemberId);
        }
    }
} 