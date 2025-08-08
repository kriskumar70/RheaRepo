using Microsoft.EntityFrameworkCore; //DbContect base class

namespace ApiProject.Models
{
    public class ApiProjectDatabaseContext : DbContext
    {
        public ApiProjectDatabaseContext(DbContextOptions options) : base(options)
        {
        //options:
        //1. database provider UseSqlServer()
        //2. connection string for sql server
        SaveChanges();
        }
        //1. ef core will look at this Dbset property to create the Deparments table in the database
        //2. DbSet provides us with all the CRUD operation methods
        public DbSet<Department> Departments { get; set; } // DbSet for Department model
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //setup of primary key
            modelBuilder.Entity<Skill>().HasKey(x => x.Id);

            //changing the column name and type
            modelBuilder.Entity<Skill>()
                .Property(x => x.Description)
                .HasColumnName("SkillDescription")
                .HasColumnType("varchar(100)")
                .IsRequired();

            //1-many configuration for Employees and Departments
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Department)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            //1-1 configuration for Employees and Profiles
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Profile)
                .WithOne(x => x.Employee)
                .HasForeignKey<Profile>(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            //many-many configuration for Employees and Skills
            modelBuilder.Entity<Employee>()
                .HasMany(x => x.Skills)
                .WithMany(x => x.Employees)
                .UsingEntity("EmpSkills");

            base.OnModelCreating(modelBuilder);
        }
    }
}
