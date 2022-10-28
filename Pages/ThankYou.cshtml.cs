using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Bakery.Pages
{
    public class ThankYouModel : PageModel
    {
        public void OnGet()
        {
            List<PurchaseProduct> PurchaseProductList = new List<PurchaseProduct>();
            HttpContext.Session.SetObjectAsJson("PurchaseProductList", PurchaseProductList);
        }
    }
}
