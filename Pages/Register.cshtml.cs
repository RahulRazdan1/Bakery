using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Bakery.Models;

namespace Bakery.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public Register Model { get; set; }
        public UserManager<IdentityUser> UserManager;
        public SignInManager<IdentityUser> SignInManager;
        public bool RefreshPage { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostSubmit()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = Model.Email,
                    Email = Model.Email
                };
                var result = await UserManager.CreateAsync(user, Model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false);
                    RefreshPage = true;
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }
    }
}
