
namespace Project
{
    public class AppointmentUI
    {
        AppointmentController appointmentObj = new AppointmentController();
        public void BookAppointment(Appointment appointment)
        {
           Console.Write(Message.inputDate);
            while (true)
            {
                DateTime date = Validation.DateValidate();
                    appointment.Date = date;
                    if (appointmentObj.BookAppointment(appointment))
                    {
                        Console.WriteLine(Message.printAppointmentBooked);
                        break;
                    }
                   else
                   {
                    Console.Write(Message.inputValidDate);
                    continue;
                   }
            }
        }
        public void ViewAppointments<T>(T user) where T : User
        {
            if (user.RoleOfUser.Equals(Role.Patient))
            {
                appointmentObj.ViewAppointment(user);
            }

            List<Appointment> AppointmentsList = appointmentObj.ViewAppointment(user);
            Console.WriteLine(Message.printViewAppointments);
            bool flag = false;
            foreach (var appointment in AppointmentsList)
            {
                if (appointment.Date >= DateTime.Now.Date)
                {
                    flag = true;
                    Console.WriteLine("Vaccine : " + appointment.VName + " on date : " + appointment.Date);
                    Console.Write(", at"+ appointment.VcName+", "+appointment.Address);
                }
            }
            if(flag==false)
                Console.WriteLine(Message.printNoAppointment);
        }
        public void CancelAppointment(Patient patientObj)
        {
            List<Appointment> PatientsAppointmentList = appointmentObj.ViewAppointment(patientObj);
            
            Console.WriteLine(Message.printViewAppointments);
            foreach (var appointments in PatientsAppointmentList)
            {
                if (appointments.Date >= DateTime.Now.Date)
                {
                    Console.WriteLine(appointments.VName + " : " + appointments.Date);
                    Console.Write(" at :" + appointments.VcName + ", " + appointments.Address);
                }
            }

        Console.Write(Message.inputDate);
            while (true)
            {
              
                DateTime date=Validation.DateValidate();
                var appointment= AuthManager<Patient>.AuthMInstance.FindAppointments(date);
               
                if (appointment == null)
                {
                    Console.WriteLine(Message.printNoAppointment);
                    return;
                }
                if (date >= DateTime.Now.Date)
                {
                    if (appointmentObj.CancelAppointment(appointment))
                    {

                        Console.WriteLine(Message.printCancelledAppointment);
                       
                    }
                }
                break;
            }
        }
    }
}
