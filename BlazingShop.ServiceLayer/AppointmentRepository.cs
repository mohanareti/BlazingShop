using BlazingShop.DataLayer;
using BlazingShop.DomainModel;
using BlazingShop.ServiceContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazingShop.ServiceLayer
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly BlazingDbContext _blazingDbContext;
        public AppointmentRepository(BlazingDbContext blazingDbContext)
        {
            _blazingDbContext = blazingDbContext;

        }
        public List<Appointment> GetAppointments => _blazingDbContext.Appointments.Include(e => e.Product).ToList();

        public bool ConfirmAppointment(int id, Appointment objAppointment)
        {
            var ExistingAppointment = _blazingDbContext.Appointments.FirstOrDefault(x => x.AId == id);
            if (ExistingAppointment != null)
            {
                ExistingAppointment.IsConfirmed = true;
                _blazingDbContext.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool CreateAppointment(int id, Appointment appointment)
        {
            appointment.ProductId = id;

            _blazingDbContext.Appointments.Add(appointment);
            _blazingDbContext.SaveChanges();
            return true;
        }

        public bool DeleteAppointment(int id)
        {
            var ExistingAppointment = _blazingDbContext.Appointments.FirstOrDefault(x => x.AId == id);
            if (ExistingAppointment != null)
            {
                _blazingDbContext.Appointments.Remove(ExistingAppointment);
                _blazingDbContext.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }

        public Appointment GetAppointmentById(int appointmentId)
        {
            return _blazingDbContext.Appointments.Include(e => e.Product).FirstOrDefault(p => p.AId == appointmentId);
        }
    }
}
