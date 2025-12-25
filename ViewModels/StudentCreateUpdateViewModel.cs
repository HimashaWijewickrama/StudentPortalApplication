using StudentPortalApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentPortalApplication.ViewModels
{
    public class StudentCreateUpdateViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100)]
        public string FullName { get; set; } = null!;


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = null!;


        [Display(Name = "Faculty")]
        [Required(ErrorMessage = "Faculty is required")]
        public int FacultyId { get; set; }


        [Display(Name = "Department")]
        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }


        [Display(Name = "Registration Date")]
        [Required(ErrorMessage = "Registration Date is required")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(StudentCreateUpdateViewModel), nameof(ValidateRegistrationDate))]
        public DateTime RegistrationDate { get; set; } = DateTime.Today;


        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(StudentCreateUpdateViewModel), nameof(ValidateDateOfBirth))]
        public DateTime DateOfBirth { get; set; } = DateTime.Today.AddYears(-60);


        [Display(Name = "Student Type")]
        [Required(ErrorMessage = "Student Type is required")]
        public int StudentTypeId { get; set; }


        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = null!;


        // dropdown lists
        public List<Faculty>? Faculties { get; set; }
        public List<Department>? Departments { get; set; }
        public List<StudentType>? StudentTypes { get; set; }

        // custom validations
        public static ValidationResult? ValidateRegistrationDate(DateTime? registrationDate, ValidationContext context)
        {
            if (!registrationDate.HasValue)
                return new ValidationResult("Registration Date is required.");

            if (registrationDate.Value.Date > DateTime.Today)
                return new ValidationResult("Registration Date cannot be in the future.");

            return ValidationResult.Success;
        }

        public static ValidationResult? ValidateDateOfBirth(DateTime? dob, ValidationContext context)
        {
            if (!dob.HasValue)
                return new ValidationResult("Date of Birth is required.");

            var minDob = DateTime.Today.AddYears(-60);  // Max age 60 years
            var maxDob = DateTime.Today.AddYears(-18);  // Min  age 18 years

            if (dob.Value.Date < minDob || dob.Value.Date > maxDob)
                return new ValidationResult($"Date of Birth must be between {minDob:yyyy-MM-dd} and {maxDob:yyyy-MM-dd}.");

            return ValidationResult.Success;
        }
    }
}