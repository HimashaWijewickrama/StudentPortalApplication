namespace StudentPortalApplication.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}