

using Newtonsoft.Json;
using Vaccine.Model;

namespace Project
{
    public class PatientDataBase
    {
        private static PatientDataBase _patientInstance;
        public static string patients;
        public List<Patient> PatientsRead;
        public static PatientDataBase PatientInstance
        {
            get
            {
                if (_patientInstance == null)
                {
                    _patientInstance = new PatientDataBase();
                }
                return _patientInstance;
            }
        }
        private PatientDataBase() 
        {
            PatientsRead = new List<Patient>();
            patients = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\Patient.json";

            try
            {
                var patientsList = File.ReadAllText(patients);
                if (!String.IsNullOrEmpty(patientsList))
                    PatientsRead = JsonConvert.DeserializeObject<List<Patient>>(patientsList);
            }
            catch
            {
                ExceptionController.DbException();
            }
            }
        }
    }

