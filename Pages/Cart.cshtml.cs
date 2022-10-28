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
    public class CartModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public List<PurchaseProduct> PurchaseProductList { get; set; }

        public CartModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            PurchaseProductList = new List<PurchaseProduct>();
        }
        public void OnGet(long ProductId)
        {
            if (HttpContext.Session != null && HttpContext.Session.GetObjectFromJson<List<PurchaseProduct>>("PurchaseProductList") == null)
                PurchaseProductList = new List<PurchaseProduct>();
            else
                PurchaseProductList = HttpContext.Session.GetObjectFromJson<List<PurchaseProduct>>("PurchaseProductList");

            ViewData["PurchaseProductCount"] = PurchaseProductList.Count.ToString();

            decimal price = 0;
            foreach(PurchaseProduct purchaseProduct in PurchaseProductList)
            {
                price = price + purchaseProduct.Quantity * purchaseProduct.Price;
            }
            ViewData["PurchasePrice"] = price;
        }

        public void OnPostPlaceYourOrder()
        {
            Response.Redirect("ThankYou");
        }
    }
}
