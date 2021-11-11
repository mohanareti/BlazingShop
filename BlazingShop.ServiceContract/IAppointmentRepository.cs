using BlazingShop.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazingShop.ServiceContract
{
   public interface IAppointmentRepository
    {
        List<Appointment> GetAppointments { get; }

        Appointment GetAppointmentById(int appointmentId);


        public bool CreateAppointment(int id, Appointment appointment);

        public bool ConfirmAppointment(int id, Appointment objAppointment);

        public bool DeleteAppointment(int id);
    }
}
