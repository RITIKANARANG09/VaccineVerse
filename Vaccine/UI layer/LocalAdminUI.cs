
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
                LocalAdminUIOptions:  Choose.LocalAdminUIChoose();
 
                int ip;
                try
                {
                    ip = Convert.ToInt32(Console.ReadLine());
                    Console.ResetColor();
                }
                catch
                {
                    ExceptionController.OnlyNumeric();
                    goto LocalAdminUIOptions;
                }
                switch (ip)
                {
                    case (int)LocalAdminChoose.ViewAvailableVaccines:
                        ViewVaccine(vaccineCenter);
                        break;
                    case (int)LocalAdminChoose.IncrementVaccines:
                        VaccineUI.IncreaseVaccineCount(vaccineCenter);
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
                var inputVal= Console.ReadLine();
                if (inputVal == "2")
                    Environment.Exit(0);
                else
                    continue;
                break;
            }

        }
       
      
            
            
        
        public static void DecreaseVaccineCount(string vc)
        {
            Console.WriteLine(Message.inputDecrementVaccineName);
            List<VaccineAvailable> VaccineNameList = VaccineController.ViewVaccinesLocally(vc);
            string result = string.Join(", ", VaccineNameList.Select(v => v.VName));
            Console.WriteLine(result);
            string vaccineName = Console.ReadLine();
            
        decDosesOptions: Console.Write($"How many doses of {vaccineName} do you want to decrease: ");
            int ivcount;
            try
            {
                ivcount = Convert.ToInt32(Console.ReadLine());
                string input = VaccineController.decrementVaccines(ivcount, vaccineName, vc);
                Console.WriteLine(input);
            }
            catch
            {
                ExceptionController.OnlyNumeric();
                goto decDosesOptions;
            }
           
        }
        public static void ViewVaccine(string vc)
        {
            List<VaccineAvailable> vaccine = VaccineController.ViewVaccinesLocally(vc);
            var result = vaccine.Select(v => $"{v.VName} : {v.vcount}");
            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        public static void PatientRecord(string vc)
        {
            Console.WriteLine("Patient Records are : ");
            List<Appointment> patientDetails=PatientController.RecordOfPatientsOfVC(vc);
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
            dosesCountOptions: Console.WriteLine("how much doses you want to add initially ? ");
                var addVaccines = Console.ReadLine();
                int idoses;
                try
                {
                    idoses = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    ExceptionController.OnlyNumeric();
                    goto dosesCountOptions;
                }
            //
            minAgeOptions: Console.Write("Enter min age for vaccine : ");
                int minAge;
                try
                {
                    minAge = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    ExceptionController.OnlyNumeric();
                    goto minAgeOptions;
                }
            //
            maxAgeOptions: Console.Write("Enter max age for vaccine : ");
                int maxAge;
                try
                {
                    maxAge = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    ExceptionController.OnlyNumeric();
                    goto maxAgeOptions;
                }

                var input = VaccineController.VaccineAddInCenter(vc, addVaccines, idoses, minAge, maxAge);
                if (input == null)
                { continue; }
                else { Console.WriteLine(input); break; }
            }

        }

    }
}
