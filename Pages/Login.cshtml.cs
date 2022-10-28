using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Bakery.Models;

namespace Bakery.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login Model { get; set; }
        public SignInManager<IdentityUser> SignInManager;

        public bool RefreshPage { get; set; }

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            SignInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostSubmit()
        {
            if (ModelState.IsValid)
            {
                
                var identiryresult = await SignInManager.PasswordSignInAsync(Model.Email,Model.Password,false, false);

                if(!identiryresult.Succeeded)
                {
                    ModelState.AddModelError("", "User Name or Password Incorrect!");
                }
                else
                {
                    RefreshPage = true;
                }
            }
            return Page();
        }
        public void OnPostForgetPwd()
        {

        }
    }
}
