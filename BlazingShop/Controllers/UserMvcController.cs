using BlazingShop.DataLayer;
using BlazingShop.DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazingShop.Controllers
{
    public class UserMvcController : Controller
    {
        private readonly ILogger<UserMvcController> _logger;
        private readonly IConfiguration _configuration;
        string apiurl;
        string apiurl2;
        BlazingDbContext _db;
        public UserMvcController(ILogger<UserMvcController> logger, IConfiguration configuration,BlazingDbContext db)
        {
            _logger = logger;
            _configuration = configuration;
            apiurl = _configuration.GetValue<string>("WebapiBaseUrl");
            apiurl2 = _configuration.GetValue<string>("WebApiUrl2");
            _db = db;

        }
        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> Details(int id)
        {
            var product = new Product();
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync($"{apiurl}/{id}"))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        product = JsonConvert.DeserializeObject<Product>(apiResponse);
                    }
                    else
                    {
                        //var noResponse = response.StatusCode.ToString();
                        ViewBag.StatusCode = response.StatusCode;

                    }
                }
            }
            return View(product);
        }
        [Authorize]
        public async Task<IActionResult> BookAppoint(int id)
        {
            
            var appoint = new Appointment();
            var product = new Product();
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(appoint), Encoding.UTF8, "application/json");
                using (var response = await client.GetAsync($"{apiurl}/{id}"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                    appoint = JsonConvert.DeserializeObject<Appointment>(apiResponse);
                    appoint.Product = product;
                    appoint.ProductId = product.PId;
                }
            }

            return View(appoint);
        }

        [HttpPost]
        public async Task<IActionResult> BookAppoint(int id, Appointment appointment)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            appointment.ApplicationUserId = userId;
            // character = new Character();
            var resappoint = new Appointment();
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, "application/json");
                    using (var response = await client.PostAsync($"{apiurl2}/{id}", content))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        resappoint = JsonConvert.DeserializeObject<Appointment>(apiResponse);
                    }


                }
                return RedirectToAction("EndPage");
           
           
        }
        public ActionResult EndPage()
        {
            return View();
        }
        [Authorize]
        public ViewResult BookingDetails()
        {
            ViewBag.Message = "Items in your Cart";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.product = _db.Products.ToList();
            var cartitems = _db.Appointments.Include(p => p.Product).Where(e => e.ApplicationUserId == userId).ToList();
            return View(cartitems);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var resAppointment = new Appointment();
            using (var client = new HttpClient())
            {

                using (var response = await client.DeleteAsync($"{apiurl2}/{id}"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    resAppointment = JsonConvert.DeserializeObject<Appointment>(apiResponse);
                }
            }
            return RedirectToAction("BookingDetails");
        }
    }
}
