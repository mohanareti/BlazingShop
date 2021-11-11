using BlazingShop.DomainModel;
using BlazingShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazingShop.Controllers
{
    public class CategoryMvcController : Controller
    {
        private readonly ILogger<CategoryMvcController> _logger;
        private readonly IConfiguration _confugration;
        string APiUrl;
        public CategoryMvcController(ILogger<CategoryMvcController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _confugration = configuration;
            APiUrl = _confugration.GetValue<string>("WebApiUrl");

        }
        public async Task<IActionResult> Index()
        {
            var Cato = new List<Category>();
            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync(APiUrl))
                {
                    var apiRespons = await response.Content.ReadAsStringAsync();
                    Cato = JsonConvert.DeserializeObject<List<Category>>(apiRespons);
                }
            }
            return View(Cato);
        }
        public ViewResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            var CreatCat = new Category();
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(APiUrl, content))
                {
                    var apiresonse = await response.Content.ReadAsStringAsync();
                    CreatCat = JsonConvert.DeserializeObject<Category>(apiresonse);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = new Category();
            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync($"{APiUrl}/{id}"))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    category = JsonConvert.DeserializeObject<Category>(apiresponse);
                }
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            var EditCateg = new Category();
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                using (var response = await client.PutAsync($"{APiUrl}/{id}", content))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    EditCateg = JsonConvert.DeserializeObject<Category>(apiresponse);
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var DelCate = new Category();
            using (var client = new HttpClient())
            {
                using (var response = await client.DeleteAsync($"{APiUrl}/{id}"))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    DelCate = JsonConvert.DeserializeObject<Category>(apiresponse);
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
