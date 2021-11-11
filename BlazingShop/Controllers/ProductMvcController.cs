using BlazingShop.DataLayer;
using BlazingShop.DomainModel;
using BlazingShop.Models;
using BlazingShop.VIewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazingShop.Controllers
{
    public class ProductMvcController : Controller
    {
        private readonly ILogger<ProductMvcController> _logger;
        private readonly IConfiguration _configuration;
        string apiurl;
        private readonly IWebHostEnvironment _webHostEnvironment;
        BlazingDbContext _blazingDb;
        public ProductMvcController(ILogger<ProductMvcController> logger, IConfiguration configuration, IWebHostEnvironment web,BlazingDbContext blazingDb)
        {
            _logger = logger;
            _configuration = configuration;
            apiurl = _configuration.GetValue<string>("WebapiBaseUrl");
            _webHostEnvironment = web;
            _blazingDb = blazingDb;

        }

        public async Task<IActionResult> Index(Product product)
        {
            var cat = new List<Product>();
            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync(apiurl))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    cat = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }

            return View(cat);
        }

        public ViewResult Create()
        {
            ViewBag.cat = _blazingDb.Categories.ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Product product = new Product
                {
                    PName = model.PName,
                    Price = model.Price,
                    ShadeColor = model.ShadeColor,
                    CategoryId = model.CategoryId,
                    Image = uniqueFileName,
                };
                var resproduct = new Product();
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                    using (var response = await client.PostAsync(apiurl, content))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        resproduct = JsonConvert.DeserializeObject<Product>(apiResponse);
                    }
                }
                return RedirectToAction(nameof(Index));
            }


            return View();
        }
        private string UploadedFile(ProductViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.cat = _blazingDb.Categories.ToList();
            var product = new Product();
            using (HttpClient client = new HttpClient())
            {


                using (var response = await client.GetAsync($"{apiurl}/{id}"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            var pvm = new ProductViewModel()
            {
                PName = product.PName,
                Price = product.Price,
                ShadeColor = product.ShadeColor,
                CategoryId = product.CategoryId,


            };
            return View(pvm);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Product product = new Product
                {
                    PId = model.PId,
                    PName = model.PName,
                    Price = model.Price,
                    ShadeColor = model.ShadeColor,
                    CategoryId = model.CategoryId,
                    Image = uniqueFileName,
                };
                var resproduct = new Product();
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8,
                        "application/json");
                    using (var response = await client.PutAsync($"{apiurl}/{id}", content))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        resproduct = JsonConvert.DeserializeObject<Product>(apiResponse);
                    }
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var resProduct = new Product();
            using (var client = new HttpClient())
            {

                using (var response = await client.DeleteAsync($"{apiurl}/{id}"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    resProduct = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
