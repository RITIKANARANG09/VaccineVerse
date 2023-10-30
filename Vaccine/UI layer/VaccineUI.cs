using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public  class VaccineUI
    {
       public static void ViewVaccine()
        {
            List<string>lists=Vaccine.ViewVaccinesGloballyCenter();
            Console.WriteLine("Vaccine available globally are : ");
            string result = string.Join(", ", lists);
            Console.WriteLine(result);
        }
        
        public static void AddVaccineByGA()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Which vaccine you want to add");
                    var addVaccine = Console.ReadLine();
                    Console.WriteLine("how much doses you want to add initially ? ");
                    var doses = Console.ReadLine();
                    int idoses = Convert.ToInt32(doses);
                    var vaccineG=Vaccine.ViewVaccinesGloballyCenter();
                    if(vaccineG.Any(v=>v.Equals(vaccineG)))
                        {
                        Console.WriteLine("Vaccine already present ");
                        continue;
                    }
                    var input = Vaccine.AddVaccineGlbally(addVaccine, idoses);
                    Console.WriteLine(input);

                }
                catch
                {
                    Console.WriteLine("Enter valid details");
                    continue;
                }
                break;
            }
        }
    }
}
