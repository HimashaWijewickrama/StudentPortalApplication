using StudentPortalApplication.Models;

namespace StudentPortalApplication.ViewModels
{
    public class StudentListViewModel
    {
        public List<Student>? Students { get; set; }
        public int TotalPages { get; set; }
        public string? SearchTerm { get; set; }
        public int? SelectedFacultyId { get; set; }
        public int? SelectedStudentTypeId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int Total { get; set; }
        public List<Faculty>? Faculties { get; set; }
        public List<StudentType>? StudentTypes { get; set; }
    }
}