
namespace Project
{
   
    public class AppointmentController :IAppointmentControllerForPatient,IAppointmentControllerForAdmin{
      //  List<Appointment> appointmentsList = ConnectToDataBase.DataBaseInstance.readAppointmentList();
        public bool BookAppointment(Appointment appointmentObject)
        {
            if (appointmentObject.Date < DateTime.Now.Date)
                return false;
            bool isBooked=AppointmentDataBase.AppointmentInstance.AddAppointment(appointmentObject);
            return isBooked;
        }
        public  bool CancelAppointment(Appointment appointmentObject)
        {
           bool isCancelled = false;
           
            foreach (var appointment in AppointmentDataBase.AppointmentInstance.AppointmentList)
            {
                if (appointment.PatientPhoneNo.Equals(appointmentObject.PatientPhoneNo) && appointment.Date==appointmentObject.Date)
                {
                    
                   isCancelled=AppointmentDataBase.AppointmentInstance.DeleteItem(appointment);
                    break;
                }
            }
           return isCancelled;
            
        }
        public List<Appointment> ViewAppointment(User user, string vaccineCenter="")
        {
            
            if (user.RoleOfUser.Equals(Role.Patient))
            {
                List<Appointment> patientAppointments = AppointmentDataBase.AppointmentInstance.AppointmentList.FindAll(a => a.PatientPhoneNo.Equals(user.PhoneNo));
                return patientAppointments;
            }
            else if (user.RoleOfUser.Equals(Role.Admin))
            {
                    List<Appointment> adminAppointments = AppointmentDataBase.AppointmentInstance.AppointmentList.FindAll(a => a.VcName.Equals(vaccineCenter));
                    return adminAppointments;
                
            }
            return null;
        }
    }
    }
