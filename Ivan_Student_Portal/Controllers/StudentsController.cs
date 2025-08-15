using Ivan_Student_Portal.Database;
using Ivan_Student_Portal.Models;
using Ivan_Student_Portal.Models.StudentTable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ivan_Student_Portal.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        // Constructor to inject the database context
        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: Show the Add Student form
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // POST: Add a new student to the database
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                
                return View(viewModel);
            }

            var student = new Student
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Location = viewModel.Location

            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }

        // GET: List all students, optionally filtered by search
        [HttpGet]
        public async Task<IActionResult> List(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var students = dbContext.Students.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(SingleStudent =>
                    SingleStudent.FirstName.Contains(searchString) ||
                    SingleStudent.LastName.Contains(searchString) ||
                    SingleStudent.Email.Contains(searchString));
            }

            var filteredStudents = await students.ToListAsync();
            return View(filteredStudents);
        }

        // GET: Show Edit form for a specific student
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);
            return View(student);
        }

        // POST: Update student details
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            if (!ModelState.IsValid)
            {
                
                return View(viewModel);
            }
            var student = await dbContext.Students.FindAsync(viewModel.Id);
            if (student != null)
            {
                student.FirstName = viewModel.FirstName;
                student.LastName = viewModel.LastName;
                student.Email = viewModel.Email;
                student.Location = viewModel.Location;
            }
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List");

        }


        // POST: Delete a student
        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await dbContext.Students.FindAsync(viewModel.Id);
            if (student != null)
            {
                dbContext.Students.Remove(student);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }
    }
}
