using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<IndexModel> _logger;
        public IList<Category> CategoryList { get; set; }
        public IList<Product> ProductList { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
            CategoryList = new List<Category>();
            ProductList = new List<Product>();
        }

        public void OnGet()
        {
            CategoryList = Category.SelectList(_configuration.GetConnectionString("DefaultConnection"), true, true);
            ProductList = Product.SelectList(_configuration.GetConnectionString("DefaultConnection"), 0, true, true);

            List<PurchaseProduct> PurchaseProductList;

            if (HttpContext.Session != null && HttpContext.Session.GetObjectFromJson<List<PurchaseProduct>>("PurchaseProductList") == null)
                PurchaseProductList = new List<PurchaseProduct>();
            else
                PurchaseProductList = HttpContext.Session.GetObjectFromJson<List<PurchaseProduct>>("PurchaseProductList");
            ViewData["PurchaseProductCount"] = PurchaseProductList.Count.ToString();
        }
    }
}
