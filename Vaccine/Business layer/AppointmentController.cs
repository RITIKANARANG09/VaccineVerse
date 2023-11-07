
using Vaccine.Model;
namespace Project
{
    public class AppointmentController {
        List<Appointment> appointmentsList = ConnectToDataBase.DataBaseInstance.readAppointmentList();
        public bool BookAppointment(Appointment appointmentObject)
        {
            bool isBooked = false;
                    isBooked=AppointmentDataBase.AppointmentInstance.AddAppointment(appointmentObject);
            if(isBooked==true)
            {

            }
            return isBooked ;
        }
        public  bool CancelAppointment(Appointment appointmentObject)
        {
           bool isCancelled = false;
            foreach (var appointment in appointmentsList)
            {
                if (appointment.patientPhoneNo == appointmentObject.patientPhoneNo && appointment.dt==appointmentObject.dt)
                {
                    
                   isCancelled=AppointmentDataBase.AppointmentInstance.DeleteItem(appointment);
                    break;
                }
            }
           return isCancelled;
            
        }
        public List<Appointment> ViewAppointment(User user, string vaccineCenter="")
        {
            
            if (user.role==Role.Patient)
            {
                List<Appointment> patientAppointments = appointmentsList.FindAll(a => a.patientPhoneNo == user.phoneNo);
                return patientAppointments;
            }
            else if (user.role == Role.Admin)
            {
                    List<Appointment> adminAppointments = appointmentsList.FindAll(a => a.VcName==vaccineCenter);
                    return adminAppointments;
                
            }
            return null;
        }
    }
    }
