using Bakery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http;
using SessionExtensions = Bakery.Models.SessionExtensions;

namespace Bakery.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IConfiguration _configuration;
        [BindProperty]
        public Product Product { get; set; }
        public ProductDetailModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            Product = new Product();
        }
        public void OnGet(long ProductId)
        {
            Product = Product.Select(_configuration.GetConnectionString("DefaultConnection"), ProductId);
            if (Product.ProductId == 0)
                Response.Redirect("../Index");
            //JSRuntime.InvokeAsync<string>("test.historyGo", string[]);

            List<PurchaseProduct> PurchaseProductList;

            if (HttpContext.Session != null && HttpContext.Session.GetObjectFromJson<List<PurchaseProduct>>("PurchaseProductList") == null)
                PurchaseProductList = new List<PurchaseProduct>();
            else
                PurchaseProductList = HttpContext.Session.GetObjectFromJson<List<PurchaseProduct>>("PurchaseProductList");
            ViewData["PurchaseProductCount"] = PurchaseProductList.Count.ToString();
        }

        public void OnPostSubmit(long ProductId)
        {
            int Quantity = Product.Quantity;
            Product = Product.Select(_configuration.GetConnectionString("DefaultConnection"), ProductId);
            List<PurchaseProduct> PurchaseProductList;

            if (HttpContext.Session != null && HttpContext.Session.GetObjectFromJson<List<PurchaseProduct>>("PurchaseProductList") == null)
                PurchaseProductList = new List<PurchaseProduct>();
            else
                PurchaseProductList = HttpContext.Session.GetObjectFromJson<List<PurchaseProduct>>("PurchaseProductList");

            if (PurchaseProductList.Any(t => t.ProductId == ProductId))
            {
                PurchaseProductList.Where(t => t.ProductId == ProductId).FirstOrDefault().Quantity += Quantity;
            }
            else
            {
                PurchaseProduct pp = new PurchaseProduct();
                pp.ProductId = ProductId;
                pp.Name = Product.Name;
                pp.Description = Product.Description;
                pp.Price = Product.Price;
                pp.Image = Product.Image;
                pp.Quantity = Quantity;
                PurchaseProductList.Add(pp);
            }
            ViewData["PurchaseProductCount"] = PurchaseProductList.Count.ToString();

            HttpContext.Session.SetObjectAsJson("PurchaseProductList", PurchaseProductList);
        }

        public void OnPostBuyNow(long ProductId)
        {
            int Quantity = Product.Quantity;
            Product = Product.Select(_configuration.GetConnectionString("DefaultConnection"), ProductId);
            List<PurchaseProduct> PurchaseProductList;

            if (HttpContext.Session != null && HttpContext.Session.GetObjectFromJson<List<PurchaseProduct>>("PurchaseProductList") == null)
                PurchaseProductList = new List<PurchaseProduct>();
            else
                PurchaseProductList = HttpContext.Session.GetObjectFromJson<List<PurchaseProduct>>("PurchaseProductList");

            if (PurchaseProductList.Any(t => t.ProductId == ProductId))
            {
                PurchaseProductList.Where(t => t.ProductId == ProductId).FirstOrDefault().Quantity += Quantity;
            }
            else
            {
                PurchaseProduct pp = new PurchaseProduct();
                pp.ProductId = ProductId;
                pp.Name = Product.Name;
                pp.Description = Product.Description;
                pp.Price = Product.Price;
                pp.Image = Product.Image;
                pp.Quantity = Quantity;
                PurchaseProductList.Add(pp);
            }
            ViewData["PurchaseProductCount"] = PurchaseProductList.Count.ToString();

            HttpContext.Session.SetObjectAsJson("PurchaseProductList", PurchaseProductList);
            Response.Redirect("../Cart");
        }
    }
}
