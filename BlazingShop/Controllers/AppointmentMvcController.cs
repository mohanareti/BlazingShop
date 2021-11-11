using BlazingShop.DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazingShop.Controllers
{
    public class AppointmentMvcController : Controller
    {
        private readonly ILogger<AppointmentMvcController> _logger;
        private readonly IConfiguration _configuration;
        string apiurl;
        public AppointmentMvcController(ILogger<AppointmentMvcController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            apiurl = _configuration.GetValue<string>("WebApiUrl2");


        }
        public async Task<IActionResult> Index(Appointment appointment)
        {
            var app = new List<Appointment>();
            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync(apiurl))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    app = JsonConvert.DeserializeObject<List<Appointment>>(apiResponse);
                }
            }

            return View(app);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var appointment = new Appointment();
            using (HttpClient client = new HttpClient())
            {

                using (var response = await client.GetAsync($"{apiurl}/{id}"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    appointment = JsonConvert.DeserializeObject<Appointment>(apiResponse);
                }
            }
            return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Appointment appointment)
        {
            var resappointment = new Appointment();
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8,
                    "application/json");
                using (var response = await client.PutAsync($"{apiurl}/{id}", content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    resappointment = JsonConvert.DeserializeObject<Appointment>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var resAppointment = new Appointment();
            using (var client = new HttpClient())
            {

                using (var response = await client.DeleteAsync($"{apiurl}/{id}"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    resAppointment = JsonConvert.DeserializeObject<Appointment>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
