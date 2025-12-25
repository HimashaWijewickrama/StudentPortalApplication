namespace StudentPortalApplication.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public ICollection<Department>? Departments { get; set; } = new List<Department>();
        public List<Student>? Students { get; set; }
    }
}