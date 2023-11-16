
using Newtonsoft.Json;

namespace Project
{
    public class AppointmentDataBase : DataBase<Appointment>, IAppointmentDataBase
    {
        private static AppointmentDataBase _appointmentInstance;
        public List<Appointment> AppointmentList;
        private static string _appointmentPath;
        public static AppointmentDataBase AppointmentInstance
        {
            get
            {
                if (_appointmentInstance == null)
                {
                    _appointmentInstance = new AppointmentDataBase();
                }
                return _appointmentInstance;
            }
        }
        private AppointmentDataBase()
        {
            AppointmentList = new List<Appointment>();
            _appointmentPath = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\Appointment.json";
            try
            {
                var appointments = File.ReadAllText(_appointmentPath);
                AppointmentList = JsonConvert.DeserializeObject<List<Appointment>>(appointments);
            }
            catch(Exception ex) 
            {
                Errors.DbException();
                ExceptionController.LogException(ex, "Error occured while reading DB");
            }
        }


        public bool AddAppointment(Appointment appointment)
        {
            return AddItem(appointment, AppointmentList, _appointmentPath);
        }
        public bool DeleteItem(Appointment appointment)
        {
            AppointmentList.Remove(appointment);
            return (UpdateItem(_appointmentPath, AppointmentList) == true);
        }
    }
}
