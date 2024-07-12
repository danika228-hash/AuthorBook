using AuthorBook.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorBook.Data;

public class AuthorBookDataContext(DbContextOptions<AuthorBookDataContext> options) 
    : DbContext(options)  
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Author>()
            .HasKey(b => b.AuthorId);

        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);  
    }
}
