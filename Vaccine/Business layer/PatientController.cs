
using Newtonsoft.Json;
using Vaccine.Model;

namespace Project
{
    public class PatientController:User
    {
        
        public static List<object> PastRecord(string phoneNumber)
        {
            List<object> patientVaccine=new List<object>();
            var readPatients = DB.DbInstance.VaccineCenterRead;
            var patientNameList = DB.DbInstance.PatientRead;
            var patientFind=patientNameList.Find(p=>p.phoneNo == phoneNumber);
            var patientName = "";
            if(patientFind != null)
            {
                patientName = patientFind.Username;
            }
            patientVaccine.Add(patientName);
            foreach (var pat in readPatients)
            {
                foreach(var v in pat.appointmentDate)
                {
                    if(v.patientPhoneNo == phoneNumber && v.dt<=DateTime.Now.Date)
                    {
                       
                        patientVaccine.Add(v.dt);
                        patientVaccine.Add(v.VName);
                    }
                }
            }
            return patientVaccine;
        }
        public static void AddPastRecordOfPatient(string phoneNumber,string VName,string centerName)
        {
            var readPatients = DB.DbInstance.PatientRead;
            foreach (var pat in readPatients)
            {
                if (pat.phoneNo == phoneNumber)
                {
                    pat.VName.Add(VName);
                    try
                    {
                        var vcJSON = JsonConvert.SerializeObject(readPatients);
                        File.WriteAllText(@"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\Patient.json", vcJSON);
                        VaccineController.decrementVaccines(1, VName, centerName);
                        break;
                    }
                    catch
                    {
                        ExceptionController.DbException();
                    }
                }
            }
        }
        public static List<Appointment> RecordOfPatientsOfVC(string VcName)
        {
            var readVaccineCenter = DB.DbInstance.VaccineCenterRead;
            List<Appointment> patientDetails = new List<Appointment>();
            foreach (var vaccineCenter in readVaccineCenter)
            {
                if (vaccineCenter.VcName == VcName)
                {
                    foreach (var appointments in vaccineCenter.appointmentDate)
                        patientDetails.Add(appointments);
                }
            }
            return patientDetails;
        }
    }
}
