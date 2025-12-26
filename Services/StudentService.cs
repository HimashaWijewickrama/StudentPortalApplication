using StudentPortalApplication.Data;
using StudentPortalApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentPortalApplication.Services
{
    public class StudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Student> Students, int TotalCount)> GetStudents(
            string? searchTerm,
            int? facultyId,
            int? studentTypeId,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Students
                .Include(e => e.Faculty)
                .Include(e => e.Department)
                .Include(e => e.StudentType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(e => e.FullName.Contains(searchTerm));
            }

            if (facultyId.HasValue && facultyId.Value > 0)
            {
                query = query.Where(e => e.FacultyId == facultyId.Value);
            }

            if (studentTypeId.HasValue && studentTypeId.Value > 0)
            {
                query = query.Where(e => e.StudentTypeId == studentTypeId.Value);
            }

            var totalCount = await query.CountAsync();

            var students = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return (students, totalCount);
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students
                .Include(e => e.Faculty)
                .Include(e => e.Department)
                .Include(e => e.StudentType)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await GetStudentByIdAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Faculty>> GetFacultiesAsync()
        {
            return await _context.Faculties.AsNoTracking().ToListAsync();
        }

        public async Task<List<StudentType>> GetStudentTypesAsync()
        {
            return await _context.StudentTypes.AsNoTracking().ToListAsync();
        }

        public async Task<List<Department>> GetDepartmentsByFacultyAsync(int facultyId)
        {
            return await _context.Departments
                .Where(f => f.FacultyId == facultyId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}