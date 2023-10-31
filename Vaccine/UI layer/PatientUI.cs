
using Vaccine.Model;

namespace Project
{
    
    public class PatientUI : User {
        public static void choose(string pn)
        {
            
            while (true)
            {
                
               Console.ForegroundColor = ConsoleColor.DarkGray;
               PatientUIOption: Choose.PatientUIChoose();
                PatientChoose input;
                try
                {
                     input = (PatientChoose)Convert.ToInt32(Console.ReadLine());

                }
                catch
                {
                    ExceptionController.OnlyNumeric();
                    goto PatientUIOption;
                }
                Console.ResetColor();
                switch (input)
                {
                    case PatientChoose.ViewVaccines:
                        chooseVC(pn);
                        break;

                    case PatientChoose.ViewPastRecords:
                        ViewPastRecordsofPatient(pn);
                        continue;


                    /*case PatientChoose.GetCertificate:
                        GetCertificateByPhoneN(pn, vname);
                        continue ;*/
                    case PatientChoose.Exit:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Enter a valid input !");
                        continue;
                }
                break;
            }
            
        }
       
        public static void PatientUIHelper(string pn)
        {
            PatientUIHelperOption: Choose.PatientUIChoose();
        PatientUIHelperOptions: HelperPatient input;
            try
            {
                input = (HelperPatient)Convert.ToInt32(Console.ReadLine());

            }
            catch
            {
                ExceptionController.OnlyNumeric();
                goto PatientUIHelperOptions;
            }
            switch (input)
            {
                case HelperPatient.Back:
                    choose(pn);
                    break;
                    case HelperPatient.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Enter a valid input!");
                    break;
            }
        }
        public static void chooseVC(string pn)
        {
            AgeOption: Console.Write("Enter age : ");
            int iage;
            try
            {
                iage = Convert.ToInt32(Console.ReadLine());

            }
            catch
            {
                ExceptionController.OnlyNumeric();
                    goto AgeOption;
            }
            Console.Write("Available vaccines are : ");
            List<string> vaccineages = ViewVaccineAgeWise(iage,pn);
            string result = string.Join(", ", vaccineages);
            Console.Write(result);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
        ChooseVaccineOption: VaccineSelectOption:  Choose.VaccineSelectChoose();
            VaccineSelect input;
            try
            {
                input = (VaccineSelect)Convert.ToInt32(Console.ReadLine());

            }
            catch
            {
                ExceptionController.OnlyNumeric();
                goto VaccineSelectOption;
            }
            if (input == VaccineSelect.Exit)
            { Environment.Exit(0); }
            else if (input == VaccineSelect.SelectVaccine)
            {
                Console.ResetColor();
                while (true)
                {

                    Console.Write("Enter vaccine name : ");
                    var vaccineN = Console.ReadLine();
                    if (!vaccineages.Contains(vaccineN))
                    {
                        Console.WriteLine("This vaccine is not of your age group ");
                        continue;
                    }
                    var type = VaccineController.FindParticularVaccineCenterWise(vaccineN);
                    if (type.Count == 0)
                    {
                        Console.WriteLine($"{vaccineN} is not available"); continue;
                    }
                    Console.WriteLine($"{vaccineN} is available at : ");
                    string types = string.Join(", ", type);
                    Console.WriteLine(types);
                    Console.Write("Which vaccination center you choose from above available centers: ");
                    var centerName = Console.ReadLine();
                    if (!type.Contains(centerName))
                    {
                        Console.WriteLine("Invalid Details ! ");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Book your appointment ");
                        AppointmentUI.bookAppointment(pn, centerName, vaccineN);
                    }
                    break;
                }
            }
            else
            {
                ExceptionController.NotValid();
                goto ChooseVaccineOption;
            }
        }
        
        public static List<object> ViewPastRecordsofPatient(string pn)
            {
           
            List<object> patients = PatientController.PastRecord(pn);
            
                Console.Write($"{patients[0]} past record of vaccines are : ");
                var patientsToPrint = string.Join("\n", patients.Skip(1));
                Console.WriteLine(patientsToPrint);
                return patients;
        }
       
        public static List<string> ViewVaccineAgeWise(int age,string pn)
        {
            List<string>vaccineages=VaccineController.ViewVaccineByAge(age);
            if (vaccineages.Count == 0)

            {
                Console.WriteLine("Currently no vaccines are available of this age group");
                PatientUIHelper("\n" + pn); }
            return vaccineages;
        }
        public static void GetCertificate(string pn,string vname)
        {
            var vaccineCenter=VaccineCenterController.GiveCertificateToPatient(pn, vname);
            Console.WriteLine($"You got vaccinated by {vname} in ");
            foreach(var read in  vaccineCenter)
            {
                Console.Write(read.VcName+" : ");
                foreach (var record in read.appointmentDate)
                {
                    if(record.patientPhoneNo == pn && record.VName==vname)
                        Console.Write(record.dt);
                }
                Console.WriteLine();

            }
        }
       /* public static void GetCertificateByPhoneN(string pn)
        {
            Console.WriteLine();
        }*/
    }
}
