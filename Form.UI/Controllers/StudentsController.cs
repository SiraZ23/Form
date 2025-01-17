using Form.UI.Data;
using Form.UI.Models;
using Form.UI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Form.UI.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone
            };
            await dbContext.students.AddAsync(student);
            await dbContext.SaveChangesAsync(); 
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students=await dbContext.students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var students = await dbContext.students.FindAsync(id);
            return View(students);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student model)
        {
            var student = await dbContext.students.FindAsync(model.Id);
            if (student != null)
            {
                student.Name = model.Name;
                student.Email = model.Email;
                student.Phone = model.Phone;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student model)
        {
            var student = await dbContext.students.AsNoTracking().
                FirstOrDefaultAsync(x=>x.Id==model.Id);
            if (student != null)
            {
                dbContext.students.Remove(model);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
    }
}
