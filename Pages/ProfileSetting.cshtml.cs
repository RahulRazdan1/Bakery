using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Bakery.Pages
{
    public class ProfileSettingModel : PageModel
    {
        public SignInManager<IdentityUser> SignInManager;

        public bool RefreshPage { get; set; }

        public ProfileSettingModel(SignInManager<IdentityUser> signInManager)
        {
            SignInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAppLogout()
        {
            await SignInManager.SignOutAsync();
            RefreshPage = true;
            return Page();
        }
    }
}
