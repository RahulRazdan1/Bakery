using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Bakery.Pages.Admin
{
    public class CategoryListModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public IList<Category> CategoryList { get; set; }
        public CategoryListModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            CategoryList = new List<Category>();
        }
        public void OnGet()
        {
            CategoryList = Category.SelectList(_configuration.GetConnectionString("DefaultConnection"), true, false);
        }
    }
}
