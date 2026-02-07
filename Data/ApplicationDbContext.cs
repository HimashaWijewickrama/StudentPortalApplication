using StudentPortalApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace StudentPortalApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<StudentType> StudentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prevent cascade delete on department foreign key
            modelBuilder.Entity<Student>()
            .HasOne(e => e.Department)
            .WithMany()
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

            // Seed studentTypes data
            modelBuilder.Entity<StudentType>().HasData(
                new StudentType { Id = 1, Name = "Foreign" },
                new StudentType { Id = 2, Name = "Local" }
            );

            // Seed faculties
            modelBuilder.Entity<Faculty>().HasData(
                new Faculty { Id = 1, Name = "IT" },
                new Faculty { Id = 2, Name = "Business" },
                new Faculty { Id = 3, Name = "Engineering" }
            );

            // Seed departments with departmentId (primary key)
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Sofware Engineering", FacultyId = 1 },
                new Department { Id = 2, Name = "Cyber Security", FacultyId = 1 },
                new Department { Id = 3, Name = "Data Science", FacultyId = 1 },

                new Department { Id = 4, Name = "Accounting", FacultyId = 2 },
                new Department { Id = 5, Name = "Human Resource Management", FacultyId = 2 },
                new Department { Id = 6, Name = "Finance", FacultyId = 2 },

                new Department { Id = 7, Name = "Civil", FacultyId = 3 },
                new Department { Id = 8, Name = "Mechanical", FacultyId = 3 },
                new Department { Id = 9, Name = "Chemical", FacultyId = 3 }
            );

            // Seed initial data
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FullName = "Himasha Wijewickrama", Email = "himasha@email.com", FacultyId = 1, DepartmentId = 1, RegistrationDate = new DateTime(2020, 1, 15), DateOfBirth = new DateTime(1998, 2, 3), StudentTypeId = 2, Gender = "Female" },
                new Student { Id = 2, FullName = "Maheesha Wijewickrama", Email = "maheesha@gmail.com", FacultyId = 1, DepartmentId = 2, RegistrationDate = new DateTime(2024, 2, 10), DateOfBirth = new DateTime(1992, 2, 15), StudentTypeId = 2, Gender = "Female" },
                new Student { Id = 3, FullName = "Mandira Liyanage", Email = "mandira.l@email.com", FacultyId = 1, DepartmentId = 3, RegistrationDate = new DateTime(2022, 11, 1), DateOfBirth = new DateTime(1999, 6, 2), StudentTypeId = 2, Gender = "Female" },
                new Student { Id = 4, FullName = "Jithma Vithanage", Email = "jib@yahoo.com", FacultyId = 2, DepartmentId = 1, RegistrationDate = new DateTime(2020, 1, 15), DateOfBirth = new DateTime(1998, 4, 21), StudentTypeId = 1, Gender = "Female" },
                new Student { Id = 5, FullName = "Tilina Ratnayake", Email = "tilinar@hotmail.com", FacultyId = 2, DepartmentId = 2, RegistrationDate = new DateTime(2019, 10, 11), DateOfBirth = new DateTime(1996, 10, 11), StudentTypeId = 1, Gender = "Male" },
                new Student { Id = 6, FullName = "Kasun Radhitha", Email = "kasun@email.com", FacultyId = 2, DepartmentId = 3, RegistrationDate = new DateTime(2018, 12, 6), DateOfBirth = new DateTime(1993, 3, 12), StudentTypeId = 2, Gender = "Male" },
                new Student { Id = 7, FullName = "Binura Yasas", Email = "binura@example.com", FacultyId = 3, DepartmentId = 1, RegistrationDate = new DateTime(2019, 6, 6), DateOfBirth = new DateTime(1996, 10, 14), StudentTypeId = 2, Gender = "Male" },
                new Student { Id = 8, FullName = "Conrad Fisher", Email = "conrad@gmail.com", FacultyId = 3, DepartmentId = 2, RegistrationDate = new DateTime(2017, 8, 30), DateOfBirth = new DateTime(1990, 10, 1), StudentTypeId = 1, Gender = "Male" },
                new Student { Id = 9, FullName = "Belly Conklin", Email = "bellyc@yahoo.com", FacultyId = 3, DepartmentId = 3, RegistrationDate = new DateTime(2017, 8, 30), DateOfBirth = new DateTime(1990, 3, 9), StudentTypeId = 1, Gender = "Female" },
                new Student { Id = 10, FullName = "Ryomen Sukuna", Email = "ryomans@yahoo.com", FacultyId = 1, DepartmentId = 1, RegistrationDate = new DateTime(2019, 9, 11), DateOfBirth = new DateTime(1994, 7, 28), StudentTypeId = 1, Gender = "Male" }
            );
        }
    }
}