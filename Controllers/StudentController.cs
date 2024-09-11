using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeMangement.Data;
using OfficeMangement.Models;
using OfficeMangement.Models.Entities;

namespace OfficeMangement.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public StudentController(ApplicationDbContext dbContext) 
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new StudentEntity
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListStudents()
        {
            var students= await _dbContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> EditStudent(Guid id)
        {
            var stud = await _dbContext.Students.FindAsync(id);
            return View(stud);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(StudentEntity studentEntity)
        {
            var stud = await _dbContext.Students.FindAsync(studentEntity.Id);
            if (stud != null)
            {
                stud.Name = studentEntity.Name;
                stud.Email = studentEntity.Email;
                stud.Phone = studentEntity.Phone;
                stud.Subscribed = studentEntity.Subscribed;
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("ListStudents", "Student");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var stud = await _dbContext.Students.FindAsync(id);
            if (stud != null)
            {
                 _dbContext.Students.Remove(stud);
                await _dbContext.SaveChangesAsync();

            }
            return RedirectToAction("ListStudents", "Student");
        }
    }
}
