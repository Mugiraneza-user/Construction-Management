using Microsoft.EntityFrameworkCore;
using mks.Models;
namespace mks.Data;


public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }


    public DbSet<User> Users { get; set; }
    public DbSet<SignupOtp> SignupOtps { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>()
            .ToTable("custom_user","dbo");


        modelBuilder.Entity<SignupOtp>()
            .ToTable("signup_otp","dbo");


    }
}