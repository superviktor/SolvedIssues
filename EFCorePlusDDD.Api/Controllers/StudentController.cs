using EFCorePlusDDD.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EFCorePlusDDD.Api.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            using (var context = new SchoolContext("connectionString", true))
            {
                var student = context.Students
                    .Include(s => s.FavouriteCourse)
                    .SingleOrDefault(s => s.Id == 1);
            }

            return null;
        }
    }
}
