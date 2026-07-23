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

    public DbSet<Role> Role { get; set; } 
    public DbSet<WorkerCategory> WorkerCategories{ get; set;}
    public DbSet<WorkerPeriod> WorkerPeriods{get; set;}


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
               entity.Property(x => x.DateJoined)
               .HasColumnName("date_joined");
                entity.Property(x => x.Disabled)
                .HasColumnName("disabled");
               entity.Property(x=> x.EmailVerified)
               .HasColumnName("email_verified");
               entity.Property(x=>x.Telephone)
                .HasColumnName("telephone");
                entity.Property(x=>x.role_id)
                .HasColumnName("role_id");
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
            
        modelBuilder.Entity <Role> (entity =>
        {
          entity.ToTable("roles", "dbo");
          entity.Property(a=>a.id)
           .HasColumnName("id");
           entity.Property(a=>a.role_name)
            .HasColumnName("role_name");
            entity.Property(a=>a.description)
            .HasColumnName("description");
            entity.Property(a=>a.is_active)
            .HasColumnName("is_active");
        }
        
         );

        modelBuilder.Entity<WorkerCategory>(entity=>
        {
          entity.ToTable("worker_category","dbo");
          entity.Property(a=>a.id);
          entity.Property(a=>a.name);
          entity.Property(a=>a.salary_per_day);
          entity.Property(a=>a.hours_per_day);
          entity.Property(a=>a.is_active);
        }


        
        ); 

        modelBuilder.Entity<WorkerPeriod>(entity =>
        {
          
          entity.ToTable("work_period","dbo");
          entity.Property(a=>a.id);
          entity.Property(a=>a.start_date);
          entity.Property(a=>a.end_date);
          entity.Property(a=>a.status);
        });


    }
}