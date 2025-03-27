using Microsoft.AspNetCore.Mvc;
using School.Manegers;
using School.ViewModels;
using School.ViewModels.Theacher;
using System.Drawing.Printing;
namespace Presentation.Controllers
{
    public class TeacherController : Controller
    {

        private TeacherManager teacherManager;
        public TeacherController(TeacherManager _teacherManager)
        {
            teacherManager = _teacherManager;
        }
        public IActionResult Index(string searchText = "",
            int pageNumber = 1,
            int pageSize = 4)
        {
            var list = teacherManager.Search(searchText: searchText,
                pageSize: pageSize,
                pageNumber: pageNumber);
            return View(list);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddTeacherViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                
                foreach (var file in viewModel.TeacherAttachments)
                {
                    FileStream fileStream = new FileStream(
                  Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Teacher", file.FileName),
                  FileMode.Create);

                    file.CopyTo(fileStream);

                    fileStream.Position = 0;

                    //save path to database;
                    viewModel.Paths.Add($"/Images/Teacher/{file.FileName}");
                }
                teacherManager.Add(viewModel.ToModel());
                return RedirectToAction("Index");
               

            }
            return View();
        }
    }
}