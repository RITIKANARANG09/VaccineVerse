using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Project
{   enum LocalAdminChoose
    {
        ViewAvailableVaccines=1,
        IncrementVaccines,
        DecrementVaccines,
        PatientRecord,
        AddVaccineToCenter
    }
    public  class LocalAdminUI 
    {
        public static void choose(string vaccineCenter)
        {
            Console.WriteLine("------------------------- Logged in as Admin ---------------------------------");
            while (true)
            {
                Console.ForegroundColor=ConsoleColor.DarkGray;
                Console.WriteLine("Choose anyone number : ");
                Console.WriteLine("1 View available Vaccines");
                Console.WriteLine("2 Increment Vaccines ");
                Console.WriteLine("3 Decrement Vaccines ");
                Console.WriteLine("4 Record of patients ");
                Console.WriteLine("5 Add vaccine to center ");
                
                int ip = Convert.ToInt32(Console.ReadLine());
                Console.ResetColor();
                switch (ip)
                {
                    case (int)LocalAdminChoose.ViewAvailableVaccines:
                        ViewVaccine(vaccineCenter);
                        break;
                    case (int)LocalAdminChoose.IncrementVaccines:
                        IncreaseVaccineCount(vaccineCenter);
                        break;
                    case (int)LocalAdminChoose.DecrementVaccines:
                        DecreaseVaccineCount(vaccineCenter);
                        break;
                    case (int)LocalAdminChoose.PatientRecord:
                        PatientRecord(vaccineCenter);
                        break;
                    case (int)LocalAdminChoose.AddVaccineToCenter:
                        AddVaccineToCenter(vaccineCenter);
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        continue;
                }
                Console.WriteLine("1 for main menu");
                Console.WriteLine("2 for exit");
                var input= Console.ReadLine();
                if (input == "2")
                    Environment.Exit(0);
                else
                    continue;
                break;
            }

        }
       
      public static void IncreaseVaccineCount(string vc)
        {
            
            Console.WriteLine("which vaccine you want to increase from available vaccines below ");
            List<VaccineAvailable> VaccineNameList = VaccineCenter.ViewVaccinesLocally(vc);
            string result = string.Join(", ", VaccineNameList.Select(v => v.VName));
            Console.WriteLine(result);
            string vaccineName = Console.ReadLine();
            Console.Write($"How many doses of {vaccineName} do you want to increase: ");
            int ivcount = Convert.ToInt32(Console.ReadLine());
            string input = VaccineCenter.incrementVaccines(ivcount, vaccineName, vc);
            Console.WriteLine(input);
            
        }
        public static void DecreaseVaccineCount(string vc)
        {
            Console.WriteLine("Which vaccine do you want to decrease from available vaccines below:");
            List<VaccineAvailable> VaccineNameList = VaccineCenter.ViewVaccinesLocally(vc);
            string result = string.Join(", ", VaccineNameList.Select(v => v.VName));
            Console.WriteLine(result);
            string vaccineName = Console.ReadLine();
            Console.Write($"How many doses of {vaccineName} do you want to decrease: ");
            int ivcount = Convert.ToInt32(Console.ReadLine());
            string input = VaccineCenter.decrementVaccines(ivcount, vaccineName, vc);
            Console.WriteLine(input);
        }
        public static void ViewVaccine(string vc)
        {
            List<VaccineAvailable> vaccine = VaccineCenter.ViewVaccinesLocally(vc);
            var result = vaccine.Select(v => $"{v.VName} : {v.vcount}");
            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        public static void PatientRecord(string vc)
        {
            Console.WriteLine("Patient Records are : ");
            List<Appointment> patientDetails=VaccineCenter.RecordOfPatientsOfVC(vc);
            var formattedAppointments = patientDetails.Select(pat => 
            $"Phone No: {pat.patientPhoneNo}, Vaccine Name: {pat.VName}, Date: {pat.dt}");
            string result = string.Join(Environment.NewLine, formattedAppointments);
            Console.WriteLine(result);
        }
        public static void AddVaccineToCenter(string vc)
        {
            while (true)
            {
                Console.Write("Which vaccine you want to add from below available vaccines : ");
                VaccineUI.ViewVaccine();
                var addVaccines = Console.ReadLine();
                Console.WriteLine("how much doses you want to add initially ? ");
                int idoses = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter min age for vaccine : ");
                int minAge = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter min age for vaccine : ");
                int maxAge = Convert.ToInt32(Console.ReadLine());
                var input = LocalAdmin.VaccineAddInCenter(vc, addVaccines, idoses,minAge,maxAge);
                if (input == "Vaccine already present in the center")

                {
                    Console.WriteLine(input);

                        continue; }
                else { Console.WriteLine(input); break; }
            }
            
        }

    }
}
