
using Vaccine.Model;

namespace Project
{

    public class VaccineCenterController
    {

        public void AddNewVaccinationCenter(User user, string vcName)
        {
            VaccineCenter vaccineCenterObject = new VaccineCenter
            {
                VcName = vcName,
                LaName = user.Username,
                username = user.Username,
                /*password = user.Password,*/
                //role = Role.Admin.ToString(),
                vaccines = new List<Vaccine>()
            };
            VaccineCenterDataBase.VaccineCenterInstance.AddVaccinationCentertoDB(vaccineCenterObject);


        }



        /*public static List<VaccineCenter> GiveCertificateToPatient(string pn,string vname)
            {
                var vaccineCenterList = ConnectToDataBase.DataBaseInstance.readVaccineCenterList();
                var vaccineCenter=vaccineCenterList.FindAll(v => v.vaccines.Equals(pn) && v.vaccines.Equals(vname));
                return vaccineCenter;
            }   

        }*/
    }
}