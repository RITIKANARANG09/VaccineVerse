using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class LocalAdmin:User
    {
        public static void viewVaccines()
        {
            Console.WriteLine("Available vaccines are : ");
            List<string> vaccines = Vaccine.ViewVaccinesGloballyCenter();
            foreach (var vaccine in vaccines)
            {
                Console.WriteLine(vaccine);
            }
        }
        public static string VaccineAddInCenter(string vc,string vaccineName,int idoses,int minAge,int maxAge)
        
        {
            var readVaccinesInVaccinationCenter = DB.DbInstance.VaccineCenterRead();
            var vaccineCenter = readVaccinesInVaccinationCenter.Find(v => v.VcName==vc);
            var vaccineFind = vaccineCenter.vaccines.Find(vacc => vacc.VName == vaccineName);
            if (vaccineFind == null)
            {
              string input=  DB.DbInstance.addVaccineInCenter(vaccineName, idoses, vc,minAge,maxAge);
                if(input != null)
                {
                    return input;
                }
                else
                {
                    return "Vaccine can't be added";
                }
            }
            else { return "Vaccine already present in the center"; }
        }
    }
}
