using Newtonsoft.Json;


namespace Project
{
    public class AppointmentController {
        public static bool bookApt(string phoneNo, string VcName, DateTime dt, string VName)
        {
            Appointment appointment = new Appointment(phoneNo, VName, dt);

            var readVaccineCenter = DB.DbInstance.VaccineCenterRead;

            foreach (var vc in readVaccineCenter)
            {
                if (vc.VcName == VcName)
                {
                    vc.appointmentDate.Add(appointment);
                    break;
                }
            }
            try {
                var appointments = JsonConvert.SerializeObject(readVaccineCenter);
                File.WriteAllText(@"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\VaccinationCenter.json", appointments);
                return true;
            }
            catch {
                ExceptionController.DbException();
                return false;
            }
        }
        public static bool cancelApt(string phoneNo, string VcName, DateTime dt)
        {

            var readVaccineCenter = DB.DbInstance.VaccineCenterRead;
            var vaccineCenter=readVaccineCenter.Find(v=>v.VcName==VcName);
            Appointment appointmentToDelete = vaccineCenter.appointmentDate.Find(appointment => appointment.dt == dt && appointment.patientPhoneNo==phoneNo);
            if(appointmentToDelete == null || dt<DateTime.Now.Date) {
                return false;
            }
            else if (appointmentToDelete != null)
                {
                    vaccineCenter.appointmentDate.Remove(appointmentToDelete);   
                }
            try
            {
                var appointments = JsonConvert.SerializeObject(readVaccineCenter);
                File.WriteAllText(@"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\VaccinationCenter.json", appointments);
                return true;
            }
            catch
            {
                ExceptionController.DbException();
                return false;
            }
        }
        /*public static List<Appointment> viewAppointment(string phoneNo)
        {

        }*/
        }
    }
