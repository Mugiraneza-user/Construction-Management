using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using mks.Dtos;
using mks.model;
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
    public DbSet<Worker> workers{get; set;}

    public DbSet<Attendance> Attendances{get; set;}

    public DbSet<Payroll> payrolls{get;set;}
    public DbSet<Creditors> Creditor {get; set;}

    public DbSet<Deduction> deductions{get;set;}

    public DbSet<Payment> payments{get;set;}
    public DbSet<PaymentBatch>PaymentBatches{get;set;}


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
          entity.Property(a=>a.wage_type)
           .HasConversion<string>();
        }


        
        ); 

        modelBuilder.Entity<WorkerPeriod>(entity =>
        {
          
          entity.ToTable("work_period","dbo");
          entity.Property(a=>a.id);
          entity.Property(a=>a.start_date);
          entity.Property(a=>a.end_date);
          entity.Property(a=>a.status)
           .HasConversion<string>();

          base.OnModelCreating(modelBuilder);
        });

        modelBuilder.Entity<Worker>(entity=>
        {
          entity.ToTable("worker","dbo");

        entity.Property(a=>a.national_id);
        entity.HasOne(a => a.Category).WithMany() .HasForeignKey(a => a.category_id) .OnDelete(DeleteBehavior.Restrict);
         entity.Property(a=>a.bank_account);
         entity.Property(a=>a.full_name);
         entity.Property(a=>a.shift)
         .HasConversion<string>();
         entity.Property(a=>a.telephone);
         entity.Property(a=>a.status)
           .HasConversion<string>();
         entity.Property(a=>a.worker_number)
           .HasComputedColumnSql("worker_number") ;
         entity.Property(a=>a.id);
         entity.Property(a=>a.date_joined)
          .HasDefaultValueSql("CURRENT_TIMESTAMP");;
        } );

      modelBuilder.Entity<Attendance>(entity =>
      {
        entity.ToTable("attendance","dbo");
         entity.Property(a=>a.id);
         entity.Property(a=>a.period_id);
         entity.Property(a=>a.worker_id);
         entity.Property(a=>a.days)
         .HasConversion( v=> JsonSerializer.Serialize(v, new JsonSerializerOptions()), v=> JsonSerializer.Deserialize<List<AttendanceDayDto>>(v, new JsonSerializerOptions()) ?? new ());
      });


      modelBuilder.Entity<Payroll> (entity =>
      {
        entity.ToTable("payroll_record", "dbo");
        entity.Property(a=>a.id);
        entity.Property(a => a.worker_id);
        entity.HasOne(a=>a.worker).WithMany().HasForeignKey(a=>a.worker_id) ;
        entity.Property(a=>a.deductions);
        entity.Property(a=>a.net_salary);
        entity.HasOne(a=>a.Period).WithMany() .HasForeignKey(a=>a.period_id);
        entity.Property(a=>a.days_worked);
        entity.Property(a=>a.status) .HasConversion<string>();


      });

      modelBuilder.Entity<Creditors>(entity=>
      {
        entity.ToTable("creditor", "dbo");
        entity.Property(a=>a.id);
        entity.Property(a=>a.name);
        entity.Property(a=>a.phone);
        entity.Property(a=>a.notes);
        entity.Property(a=>a.created_at)
         .HasDefaultValueSql("CURRENT_TIMESTAMP");
      });

    modelBuilder.Entity<Deduction>(entity =>
    {
      entity.ToTable("worker_deduction","dbo");
        entity.Property(a=>a.id);
        entity.Property(a=>a.created_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
        entity.Property(a=>a.amount);
        entity.Property(a=>a.updated_at) .HasDefaultValueSql("CURRENT_TIMESTAMP");
        entity.Property(a=>a.creditor_id);
        entity.HasOne(a=>a.creditors).WithMany().HasForeignKey(a=>a.creditor_id);
        entity.Property(a=>a.worker_id);
        entity.HasOne(a=>a.worker).WithMany() .HasForeignKey(a=>a.worker_id);
        entity.Property(a=>a.status) .HasConversion<string>();
        
    });

    modelBuilder.Entity<Payment>(entity =>
    {
      entity.ToTable("payment", "dbo");

      entity.Property(a=>a.id);
      entity.Property(a=>a.payment_method) .HasConversion<string>();
      entity.Property(a=>a.payment_number);
      entity.Property(a=>a.worker_id);
      entity.HasOne(a=>a.worker).WithMany() .HasForeignKey(a=>a.worker_id);
      entity.Property(a=>a.period_id);
      entity.HasOne(a=>a.period).WithMany() .HasForeignKey(a=>a.period_id);
      entity.Property(a=>a.Payment_date) .HasDefaultValueSql("CURRENT_TIMESTAMP");
      entity.Property(a=>a.payroll_id);
      entity.HasOne(a=>a.payroll).WithMany().HasForeignKey(a=>a.payroll_id);
      entity.Property(a=>a.notes);
      entity.Property(a=>a.status) .HasConversion<string>();

      
    });

    modelBuilder.Entity<PaymentBatch>(entity =>
    {
      entity.ToTable("payment_batch","dbo");

      entity.Property(a=>a.id);
      entity.Property(a=>a.category_id);
      entity.HasOne(a=>a.category).WithMany() .HasForeignKey(a=>a.category_id);
      entity.Property(a=>a.payment_date) .HasDefaultValueSql("CURRENT_TIMESTAMP");
      entity.Property(a=>a.total_amount);
      entity.Property(a=>a.notes);
      entity.Property(a=>a.payment_method) .HasConversion<string>();
      entity.Property(a=>a.period_id);
      entity.HasOne(a=>a.period).WithMany().HasForeignKey(a=>a.period_id);
      entity.Property(a=>a.status) .HasConversion<string>();
      entity.Property(a=>a.batch_number);
    });
    }
}