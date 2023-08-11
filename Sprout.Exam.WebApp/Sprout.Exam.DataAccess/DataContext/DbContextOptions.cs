using Microsoft.EntityFrameworkCore;

namespace Sprout.Exam.DataAccess.DataContext
{
    public class DbContextOptions
    {
        public static DbContextOptions<EmployeeDbContext> Options { get; set; }
    }
}
