using Lesson49MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Lesson49MVC.ViewModels;

namespace Lesson49MVC
{
    public class MobileContext: IdentityDbContext<User>
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }
        


        public MobileContext(DbContextOptions<MobileContext> options)
                :base(options)
        {

        }
        


        public DbSet<Lesson49MVC.ViewModels.EditUserViewModel> EditUserViewModel { get; set; }
    }
}
