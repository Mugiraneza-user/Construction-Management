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

        modelBuilder.Entity<User>( entity=>
        {
           entity.ToTable("custom_user","dbo");

             entity.Property(x => x.Id)
              .HasColumnName("id");
              entity.Property(x => x.FirstName)
              .HasColumnName("first_name");
              entity.Property(x => x.LastName)
              .HasColumnName("last_name");
              entity.Property(x => x.LastLogin)
              .HasColumnName("last_login");
              entity.Property(x => x.Email)
              .HasColumnName("email");
              entity.Property(x => x.Username)
              .HasColumnName("username");
              entity.Property(x => x.IsActive)
              .HasColumnName("is_active");
              entity.Property(x => x.IsStaff)
              .HasColumnName("is_staff");
               entity.Property(x => x.IsSuperuser)
              .HasColumnName("is_superuser");
               entity.Property(x => x.WorkerId)
              .HasColumnName("worker_id");
               entity.Property(x => x.Role)
              .HasColumnName("role");
               entity.Property(x => x.DateJoined)
               .HasColumnName("date_joined");
                entity.Property(x => x.Disabled)
                .HasColumnName("disabled");
               entity.Property(x=> x.EmailVerified)
               .HasColumnName("email_verified");
        })  ;

        modelBuilder.Entity<SignupOtp>(entity =>
        {
            entity.ToTable("signup_otp","dbo");
            entity.Property(x=> x.CreatedAt)
              .HasColumnName("created_at");
            entity.Property(x=> x.ExpiresAt)
              .HasColumnName("expires_at");
            entity.Property(x=> x.IsUsed)
               .HasColumnName("is_used");
        });
            


    }
}