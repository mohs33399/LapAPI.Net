using ConsoleApp1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Manegers;
using School.ViewModels.Theacher;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/{controllers}")]

    public class TeacherController : Controller
    {

        private TeacherManager teacherManager;
        public TeacherController(TeacherManager _teacherManager)
        {
            teacherManager = _teacherManager;
        }

        [Route("index")]
        public IActionResult Index(string searchText = "",
            int pageNumber = 1,
            int pageSize = 4)
        {
            var list = teacherManager.Search(searchText: searchText,
                pageSize: pageSize,
                pageNumber: pageNumber);
            return Ok(list);
        }

        [Authorize(Roles = "Teacher")]
        [Route("TeacherList")]
        public IActionResult TeacherList(string searchText = "", decimal price = 0,
           int categoryId = 0, int pageNumber = 1,
           int pageSize = 3)

        {

            var myID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var list = teacherManager.Search(searchText: searchText,
                pageSize: pageSize,
                pageNumber: pageNumber);
            return Ok(list);

        }





        [HttpGet]
        public IActionResult Add()
        {
            return Ok();
        }

        [HttpPost]
        [Route("add")]
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
                return Ok(new { massage = "Successfull added" });


            }
            return Ok(new { massage = "Data is invaild" });
        }

       

    }
}

