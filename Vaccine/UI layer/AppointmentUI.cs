
using Vaccine.Model;
namespace Project
{
    public class AppointmentUI
    {
        AppointmentController appointmentObj = new AppointmentController();
        public void bookAppointment(Appointment appointment)
        {
        ValidD: Console.Write(Message.inputDate);
            while (true)
            {
                DateTime date = Validation.DateValidate();
               
                if (date >= DateTime.Now.Date)
                {
                    appointment.dt = date;
                    if (appointmentObj.BookAppointment(appointment))
                    {

                        Console.WriteLine(Message.printAppointmentBooked);
                        break;
                    }
                }
                else
                {
                    if (date < DateTime.Now.Date)
                    {
                        Console.Write(Message.inputValidDate);
                        continue;
                    }
                    Console.Write(Message.inputValidDate);
                    continue;
                }

             
            }


        }
        public void viewAppointments<T>(T user) where T : User
        {
            if (user.role == Role.Patient)
            {
                appointmentObj.ViewAppointment(user);
            }

            List<Appointment> appointments = appointmentObj.ViewAppointment(user);
            Console.WriteLine(Message.printViewAppointments);
            if (appointments.Count == 0)
            {
                Console.WriteLine(Message.printNoAppointment);
                return;
            }
            foreach (var appointment in appointments)
            {
                if (appointment.dt >= DateTime.Now.Date)
                {
                    Console.WriteLine("Vaccine : " + appointment.VName + " on date : " + appointment.dt);
                }
            }
        }
        public void CancelAppointment(Patient patientObj)
        {
            List<Appointment> patientsAppointmentList = appointmentObj.ViewAppointment(patientObj);
            Console.WriteLine(Message.printViewAppointments);

            foreach (var appointments in patientsAppointmentList)
            {
                if (appointments.dt >= DateTime.Now.Date)
                    Console.WriteLine(appointments.VName + " : " + appointments.dt);
            }

        ValidD: Console.Write(Message.inputDate);
            while (true)
            {
              
                DateTime date=Validation.DateValidate();
                Appointment appointment = patientsAppointmentList.Find(v => v.dt == date);

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
