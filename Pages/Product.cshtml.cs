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
    public class ProductModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public IList<Product> ProductList { get; set; }
        public ProductModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            ProductList = new List<Product>();
        }
        public void OnGet(long CategoryId)
        {
            ProductList = Product.SelectList(_configuration.GetConnectionString("DefaultConnection"), CategoryId, true, false);
        }
    }
}
