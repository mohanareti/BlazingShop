using BlazingShop.DomainModel;
using BlazingShop.ServiceContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingShop.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentApiController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IProductRepository _productRepository;
        public AppointmentApiController(IAppointmentRepository appointmentRepository, IProductRepository productRepository)
        {
            _appointmentRepository = appointmentRepository;
            _productRepository = productRepository;

        }

        public IActionResult Get()
        {
            var appointments = _appointmentRepository.GetAppointments.ToList();

            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var appointment = _appointmentRepository.GetAppointmentById(id);
            return Ok(appointment);
        }

        [HttpPost("{id}")]

        public IActionResult Post(int id, [FromBody] Appointment appointment)
        {
            var result = _productRepository.GetProductById(id);

            _appointmentRepository.CreateAppointment(result.PId, appointment);

            return Ok();

        }

        [HttpPut("{id}")]

        public IActionResult Put(int id, [FromBody] Appointment appointment)
        {
            var result = _appointmentRepository.GetAppointmentById(id);
            if (result == null)
            {
                return BadRequest($"Appointment with id {id.ToString()} not found");
            }
            else
            {
                _appointmentRepository.ConfirmAppointment(id, appointment);

                return Ok();
            }
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var result = _appointmentRepository.GetAppointmentById(id);
            if (result == null)
            {
                return BadRequest($"Appointment with id {id.ToString()} not found");
            }
            else
            {
                _appointmentRepository.DeleteAppointment(id);
                return Ok();
            }
        }

    }
}
