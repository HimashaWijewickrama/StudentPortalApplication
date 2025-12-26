using StudentPortalApplication.Models;
using StudentPortalApplication.Services;
using StudentPortalApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace StudentPortalApplication.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> List(
            string? searchTerm,
            int? SelectedFacultyId,
            int? SelectedStudentTypeId,
            int pageNumber = 1,
            int pageSize = 5)
        {
            var (students, totalCount) = await _studentService.GetStudents(searchTerm, SelectedFacultyId, SelectedStudentTypeId, pageNumber, pageSize);

            var viewModel = new StudentListViewModel
            {
                Students = students,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Total = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                SearchTerm = searchTerm,
                SelectedFacultyId = SelectedFacultyId,
                SelectedStudentTypeId = SelectedStudentTypeId,
                Faculties = await _studentService.GetFacultiesAsync(),
                StudentTypes = await _studentService.GetStudentTypesAsync()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new StudentCreateUpdateViewModel
            {
                Faculties = await _studentService.GetFacultiesAsync(),
                StudentTypes = await _studentService.GetStudentTypesAsync(),
                Departments = new List<Department>()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    FullName = vm.FullName,
                    Email = vm.Email,
                    FacultyId = vm.FacultyId,
                    DepartmentId = vm.DepartmentId,
                    RegistrationDate = vm.RegistrationDate,
                    DateOfBirth = vm.DateOfBirth,
                    StudentTypeId = vm.StudentTypeId,
                    Gender = vm.Gender
                };

                await _studentService.CreateStudentAsync(student);
                return RedirectToAction("Success", new { id = student.Id });
            }

            vm.Faculties = await _studentService.GetFacultiesAsync();
            vm.StudentTypes = await _studentService.GetStudentTypesAsync();
            vm.Departments = vm.FacultyId != 0 ? await _studentService.GetDepartmentsByFacultyAsync(vm.FacultyId) : new List<Department>();

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();

            var vm = new StudentCreateUpdateViewModel
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                FacultyId = student.FacultyId,
                DepartmentId = student.DepartmentId,
                RegistrationDate = student.RegistrationDate,
                DateOfBirth = student.DateOfBirth,
                StudentTypeId = student.StudentTypeId,
                Gender = student.Gender,
                Faculties = await _studentService.GetFacultiesAsync(),
                StudentTypes = await _studentService.GetStudentTypesAsync(),
                Departments = await _studentService.GetDepartmentsByFacultyAsync(student.FacultyId)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(StudentCreateUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Id = vm.Id!.Value,
                    FullName = vm.FullName,
                    Email = vm.Email,
                    FacultyId = vm.FacultyId,
                    DepartmentId = vm.DepartmentId,
                    RegistrationDate = vm.RegistrationDate,
                    DateOfBirth = vm.DateOfBirth,
                    StudentTypeId = vm.StudentTypeId,
                    Gender = vm.Gender
                };

                await _studentService.UpdateStudentAsync(student);
                TempData["Message"] = $"Student with ID {student.Id} and Name {student.FullName} has been updated.";
                return RedirectToAction("List");
            }

            vm.Faculties = await _studentService.GetFacultiesAsync();
            vm.StudentTypes = await _studentService.GetStudentTypesAsync();
            vm.Departments = vm.FacultyId != 0 ? await _studentService.GetDepartmentsByFacultyAsync(vm.FacultyId) : new List<Department>();

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();

            await _studentService.DeleteStudentAsync(id);
            TempData["Message"] = $"Student with ID {id} and Name {student.FullName} has been deleted.";
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<JsonResult> GetDepartments(int facultyId)
        {
            var departments = await _studentService.GetDepartmentsByFacultyAsync(facultyId);
            var result = departments.Select(d => new { id = d.Id, name = d.Name }).ToList();
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Success(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }
    }
}