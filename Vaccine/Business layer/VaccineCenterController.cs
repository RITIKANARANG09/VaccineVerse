
using Vaccine.Model;

namespace Project
{
    
    public class VaccineCenterController
    {
        
        public static void AddNewVaccinationCenter(User user, string vcName)
        {
            VaccineCenter vc = new VaccineCenter
            {
                VcName = vcName,
                LaName = user.Username,
                username = user.Username,
                password = user.Password,
                role = Role.Admin.ToString(),
                vaccines = new List<VaccineAvailable>(),
                appointmentDate=new List<Appointment>()
            };

            DB.DbInstance.AddVaccinationCentertoDB(vc);

        }
       
        
        public static List<VaccineCenter> GiveCertificateToPatient(string pn,string vname)
        {
            var vaccineCenterRead = DB.DbInstance.VaccineCenterRead;
            var vaccineCenter=vaccineCenterRead.FindAll(v => v.vaccines.Equals(pn) && v.vaccines.Equals(vname));
            return vaccineCenter;
        }   
        
    }
}