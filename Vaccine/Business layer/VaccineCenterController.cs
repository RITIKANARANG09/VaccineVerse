

namespace Project
{

    public class VaccineCenterController:IAdd<VaccineCenter>
    {

        public bool Add(VaccineCenter vaccineCenterObject)
        {
            return VaccineCenterDataBase.VaccineCenterInstance.AddVaccinationCentertoDB(vaccineCenterObject);
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
