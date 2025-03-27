using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;

namespace School.ViewModels.Theacher
{
     public static class TeacherExt
    {

        public static Teacher ToModel(this AddTeacherViewModel teacher)
        {
            return new Teacher
            {
                Name = teacher.Name,
                Subject = teacher.Subject,
                Email = teacher.Email,
                Phone = teacher.Phone,
                TeacherAttachments = teacher.Paths.Select(path => new TeacherAttachment() { Image = path }).ToList()
            };
        }

       
        public static TeacherDetailsViewModel ToDetails(this Teacher teacher)
        {
            return new TeacherDetailsViewModel
            {

                Name = teacher.Name,
                Subject = teacher.Subject,
                Email = teacher.Email,
                Phone = teacher.Phone,
                Images = teacher.TeacherAttachments != null ? teacher.TeacherAttachments.Select(attachment => attachment.Image).ToList() : new List<string>()
                //Images = teacher.TeacherAttachments.Select(attachment => attachment.Image).ToList() ?? new List<string>()
            };
        }
} 
}

