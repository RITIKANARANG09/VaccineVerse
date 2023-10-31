using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project { 
    public class AppointmentUI
    {
    public static void bookAppointment(string pn, string centerName, string vname)
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
            catch { Console.WriteLine("Enter a valid datetime "); goto ValidD; }
            if (DateTime.TryParse(input, out DateTime userDate) && dt >= DateTime.Now.Date)
            {
                if (AppointmentController.bookApt(pn, centerName, dt, vname))
                {
                    PatientController.AddPastRecordOfPatient(pn, vname, centerName);
                    Console.WriteLine("Appointment booked successfully");
                }
            }
            else
            {
                if (dt < DateTime.Now.Date)
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
        var inp = Console.ReadLine();
        /*switch (inp)
        {
            case "1":
                GetCertificate(pn, vname); break;
            case "2":
                Environment.Exit(0);
                break;
        }*/
    }
        public static void viewAppointment(string pn)
        {

        }
        public static void CancelAppointment(string pn, string centerName,Appointment appointment)
        {
            Console.WriteLine(Message.inputDateCancelApt);
            var input=Console.ReadLine();
            DateTime dt = DateTime.Parse(input);
            if(appointment.dt!=dt)
            {
                Console.WriteLine("You don't have any appointments on this particular date !");
                return;
            }
            if (DateTime.TryParse(input, out DateTime userDate) && dt <= DateTime.Now.Date)
            {
                if (AppointmentController.cancelApt(pn, centerName, dt))
                {
                   // PatientController.AddPastRecordOfPatient(pn, vname, centerName);
                    Console.WriteLine("Appointment booked successfully");
                }
            }

        }
}
}
