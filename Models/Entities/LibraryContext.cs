using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Models.Entities
{
    public class LibraryContext:DbContext
    {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=LibraryProjectDb;integrated security=true;");
            }

            public DbSet<Book> Books { get; set; }
            public DbSet<BorrowedBook> BorrowedBooks { get; set; }
            public DbSet<Member> Members { get; set; }
            public DbSet<Author> Authors { get; set; }
            public DbSet<Genre> Genres { get; set; }
            public DbSet<Employee> Employees { get; set; }


    }
}

