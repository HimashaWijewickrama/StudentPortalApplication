namespace StudentPortalApplication.Models
{
    public class StudentType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public List<Student>? Students { get; set; }
    }
}