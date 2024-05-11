using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhoneBook.WebApi.Models;

namespace PhoneBook.WebApi.Data;

public class PhonebookDbContext : IdentityDbContext<AppUser>
{
    public PhonebookDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        List<IdentityRole> roles =
        [
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            },
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            }
        ];

        builder.Entity<IdentityRole>().HasData(roles);
    }

    public DbSet<PhonebookRecord> PhonebookRecords { get; set; }
    public DbSet<PhoneNumber> PhoneNumbers { get; set; }
}