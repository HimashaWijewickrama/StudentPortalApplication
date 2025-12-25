using StudentPortalApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentPortalApplication.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name cannot be longer than 100 characters")]
        public string FullName { get; set; } = null!;


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;


        [Required(ErrorMessage = "Faculty is required")]
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; } = null!;


        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;


        [Required(ErrorMessage = "Registration is required")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }


        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "Student Type is required")]
        public int StudentTypeId { get; set; }
        public StudentType StudentType { get; set; } = null!;


        [Required(ErrorMessage = "Gender is required")]
        [StringLength(6, ErrorMessage = "Gender should be Male, Female, or Other")]
        public string Gender { get; set; } = null!;
    }
}