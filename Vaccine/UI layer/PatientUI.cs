using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    enum PatientChoose
    {
        ViewVaccines=1,
        ViewPastRecords,
        GetCertificate
    }
    public class PatientUI : User {
        public static void choose(string pn)
        {
            
            while (true)
            {
                
               Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Choose any one : ");
                Console.WriteLine("1. View Vaccines");
                Console.WriteLine("2. View Past Records");
                //Console.WriteLine("3. Get Certificates");
                var input = Convert.ToInt32(Console.ReadLine());
                Console.ResetColor();
                switch (input)
                {
                    case (int)PatientChoose.ViewVaccines:
                        chooseVC(pn);
                        //PatientUIHelper(pn);
                        break;
                    case (int)PatientChoose.ViewPastRecords:
                       
                        ViewPastRecordsofPatient(pn);
                        PatientUIHelper(pn);
                        break;
                    /*case (int)PatientChoose.GetCertificate:
                        GetCertificateByPhoneN(pn,vname);
                        break;*/

                }
                break;
            }
            
        }
        enum HelperPatient
        {
           Back=1,
           Exit
        }
        public static void PatientUIHelper(string pn)
        {
            Console.WriteLine("Press 1 to go back");
            Console.WriteLine("Press 2 to exit");
            var input=Convert.ToInt32(Console.ReadLine());
            switch(input)
            {
                case (int)HelperPatient.Back:
                    choose(pn);
                    break;
                    case (int)HelperPatient.Exit:
                    Environment.Exit(0);
                    break;
            }
        }
        public static void chooseVC(string pn)
        {
            Console.Write("Enter age : ");
            int iage = Convert.ToInt32(Console.ReadLine());
            Console.Write("Available vaccines are : ");
            List<string> vaccineages = ViewVaccineAgeWise(iage,pn);
            string result = string.Join(", ", vaccineages);
            Console.Write(result);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Select vaccine :");
            Console.WriteLine("2. Exit");
            var input = Console.ReadLine();
            if (input == "2")
            { Environment.Exit(0); }
            Console.ResetColor();
            while (true)
            {
                
                Console.Write("Enter vaccine name : ");
                var vaccineN = Console.ReadLine();
                if(!vaccineages.Contains(vaccineN))
                {
                    Console.WriteLine("This vaccine is not of your age group ");
                    continue;
                }
                var type = Vaccine.FindVaccineCenterWise(vaccineN);
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
                    bookAppointment(pn, centerName,vaccineN);
                }
                break;
            }
        }
        public static void bookAppointment(string pn, string centerName,string vname)
        {
        ValidD: Console.Write("Enter a date (e.g., '2023-10-27'): ");
            while (true)
            {
                string input = "";
                DateTime dt;
                try
                {
                    input = Console.ReadLine();
                     dt = DateTime.Parse(input);

                }
                catch (Exception e) { Console.WriteLine("Enter a valid datetime "); goto ValidD; }
                if (DateTime.TryParse(input, out DateTime userDate) && dt>=DateTime.Now.Date)
                {
                    if (Appointment.bookApt(pn, centerName, dt,vname))
                    { Patient.AddPastRecordOfPatient(pn,vname,centerName);
                        Console.WriteLine("Appointment booked successfully"); }
                }
                else
                {
                    if(dt<DateTime.Now.Date)
                    {
                        Console.Write(" Please enter a valid date :");
                        continue;
                    }
                    Console.Write("Invalid date format. Please enter a valid date :");
                    continue;
                }
                break;
            }
            Console.WriteLine("Press 1 to download certificate : ");
            Console.WriteLine("Press 2 to exit :");
            var inp= Console.ReadLine();
            switch(inp)
            {
                case "1":
                    GetCertificate(pn,vname); break;
                case "2":
                    Environment.Exit(0);
                    break;
            }
        }
        public static List<object> ViewPastRecordsofPatient(string pn)
            {
           
            List<object> patients = Patient.PastRecord(pn);
            
                Console.Write($"{patients[0]} past record of vaccines are : ");
                var patientsToPrint = string.Join(", ", patients.Skip(1));
                Console.WriteLine(patientsToPrint);
                return patients;
        }
       
        public static List<string> ViewVaccineAgeWise(int age,string pn)
        {
            List<string>vaccineages=VaccineCenter.ViewVaccineByAge(age);
            if (vaccineages.Count == 0)

            {
                Console.WriteLine("Currently no vaccines are available of this age group");
                PatientUIHelper("\n" + pn); }
            return vaccineages;
        }
        public static void GetCertificate(string pn,string vname)
        {
            var vaccineCenter=VaccineCenter.GiveCertificateToPatient(pn, vname);
            Console.WriteLine("You got vaccinated by {vname} in ");
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
