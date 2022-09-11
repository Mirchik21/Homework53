using Lesson49MVC.Models;
using Lesson49MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson49MVC.Controllers
{
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        private readonly DateTime EndDate;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            EndDate = new DateTime(2222, 06, 06);
        }

        [Authorize(Roles = "admin")]
        public IActionResult ListOfUsers()
        {
            return View(_userManager.Users.ToList());
        }


        public bool LockUser(string Id, DateTime? endDate)
        {
            if (endDate == null)
                endDate = EndDate;

            var userTask = _userManager.FindByIdAsync(Id);
            userTask.Wait();
            var user = userTask.Result;

            var lockUserTask = _userManager.SetLockoutEnabledAsync(user, true);
            lockUserTask.Wait();

            var lockDateTask = _userManager.SetLockoutEndDateAsync(user, endDate);
            lockDateTask.Wait();

            return lockDateTask.Result.Succeeded && lockUserTask.Result.Succeeded;
        }


        public async Task<IActionResult> Edit(string Id)
        {
            User user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                
                var userRoles = await _userManager.GetRolesAsync(user);
               
                var allRoles = _roleManager.Roles.ToList();
             
                var addedRoles = roles.Except(userRoles);
                
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Index", "Home");
            }

            return NotFound();
        }
    }
}
    

