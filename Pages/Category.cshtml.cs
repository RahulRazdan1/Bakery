using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Bakery.Pages
{
    public class CategoryModel : PageModel
    {

        private readonly IConfiguration _configuration;
        public IList<Category> CategoryList { get; set; }
        public CategoryModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            CategoryList = new List<Category>();
        }
        public void OnGet()
        {
            CategoryList = Category.SelectList(_configuration.GetConnectionString("DefaultConnection"), true, false);

            List<PurchaseProduct> PurchaseProductList;

            if (HttpContext.Session != null && HttpContext.Session.GetObjectFromJson<List<PurchaseProduct>>("PurchaseProductList") == null)
                PurchaseProductList = new List<PurchaseProduct>();
            else
                PurchaseProductList = HttpContext.Session.GetObjectFromJson<List<PurchaseProduct>>("PurchaseProductList");
            ViewData["PurchaseProductCount"] = PurchaseProductList.Count.ToString();
        }
    }
}
