using Vaccine.Model;

namespace Project
{
    public class ConnectToDataBase
    {
        private static ConnectToDataBase _dataBaseInstance;
        private List<VaccineCenter> _toVaccineCenterDb;
        private List<Vaccine> _toVaccineDb;
        private List<Appointment> _toAppointmentDB;
        private List<Patient> _toPatientDB;
        private List<User> _toUserDB;
        public static ConnectToDataBase DataBaseInstance
        {
            get
            {
                if (_dataBaseInstance == null)
                {
                    _dataBaseInstance = new ConnectToDataBase();
                }
                return _dataBaseInstance;
            }
        }
        private ConnectToDataBase() 
        {
         _toVaccineCenterDb = VaccineCenterDataBase.VaccineCenterInstance.VaccineCenterList;
         _toVaccineDb = VaccineDataBase.VaccineInstance.vaccineList;
         _toAppointmentDB = AppointmentDataBase.AppointmentInstance.AppointmentList;
         _toPatientDB = PatientDataBase.PatientInstance.PatientsRead;
         _toUserDB = UserDataBase.UserInstance.UsersList;
    
    }
       
        public List<Vaccine> readVaccineList()
        {
            return _toVaccineDb;
        }
        public List<VaccineCenter> readVaccineCenterList()
        {
            return _toVaccineCenterDb;
        }
        public List<Patient> readPatientList()
        {
            return _toPatientDB;
        }
        public List<User> readUsersList()
        {
            return _toUserDB;
        }
        public List<Appointment> readAppointmentList()
        {
            return _toAppointmentDB;
        }
    }
}
