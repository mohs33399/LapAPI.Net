using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using LinqKit;
using Microsoft.IdentityModel.Tokens;
using School.ViewModels;
using School.ViewModels.Theacher;
namespace School.Manegers
{
    public class TeacherManager:BaseManager<Teacher>
    {
        public TeacherManager(SchoolDbContext context): base(context) {
        }
        public PaginationViewModel<TeacherDetailsViewModel> Search(
            string searchText = "", int pageNumber = 1, int pageSize = 4)
        {
            var builder = PredicateBuilder.New<Teacher>();

            var old = builder;


            if (!searchText.IsNullOrEmpty()) {
                builder = builder.And(i => i.Name.ToLower().Contains(searchText.ToLower()) ||
               i.Subject.ToLower().Contains(searchText.ToLower()) ||
               i.Email.ToLower().Contains(searchText.ToLower()) ||
               i.Phone.ToLower().Contains(searchText.ToLower()));
            }
            if (old == builder) {
                builder = null;
            
            }

            var count = base.GetList(builder).Count();

            var ResultAfterPagination = base.Get(
                filter: builder,
                pageSize: pageSize,
                pageNumber: pageNumber)
                .Select(p => p.ToDetails()).ToList();

            return new PaginationViewModel<TeacherDetailsViewModel>
            {
                Data = ResultAfterPagination,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Total = count
            };


        }
        
        
        

        



    }
}
