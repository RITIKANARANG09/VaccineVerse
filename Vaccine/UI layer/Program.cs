using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;
using Vaccine.UI_layer;

namespace Project
{

    class Proj
    {
        public static void Design(string text)
        {
            int boxWidth = 40; // Set the width of the box
            int boxHeight = 5; // Set the height of the box
           

            // Calculate the number of spaces on each side to center the text
            int padding = (boxWidth - text.Length) / 2;

            // Print the top of the box
            Console.WriteLine(new string('-', boxWidth));

            // Print the sides of the box with text in the middle
            for (int i = 0; i < boxHeight - 2; i++)
            {
                Console.Write("|");
                Console.Write(new string(' ', padding - 1));
                if (i == (boxHeight - 2) / 2)
                    Console.Write(" " + text + " ");
                Console.Write(new string(' ', padding - 1));
                Console.Write("|");
                Console.WriteLine();
            }
        }
        enum StartFunction
        {
            Login = 1,
            Signup,
            ViewVaccines,
            Exit
        }
        public static void Main(String[]args)
        { 
            MainFunction();
        }
        public static void MainFunction()
        {
            while (true)
            {
                mainOptions:  Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Choose a number:");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Signup (For Patients) ");
                Console.WriteLine("3. View Vaccines");
                Console.WriteLine("4. Exit");
                int choice;
                try
                {
                     choice = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Errors.OnlyNumeric();
                    goto mainOptions;
                }
                
                Console.ResetColor();
                switch (choice)
                {
                    case (int)StartFunction.Login:

                        List<string> data = LoginUI.UserLogin();
                        if (data[0] == Role.Admin.ToString())
                        {
                            AdminLogin(Role.Admin.ToString());
                        }
                        else if (data[0] == Role.GlobalAdmin.ToString())
                        {
                            Console.WriteLine("--------------------- Global Admin ------------------------------");
                            GlobalAdminUI.choose();
                        }
                        else if (data[0] == Role.Patient.ToString())
                        {
                            Console.WriteLine("---------------------------- Patient --------------------------------");
                            PatientUI.choose(data[1]);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Credentials ! ");
                            continue;
                        }
                        break;

                    case (int)StartFunction.Signup:
                        PatientSignUp();
                        break;

                    case (int)StartFunction.ViewVaccines:
                        VaccineUI.ViewVaccine();
                        continue;
                    case (int)StartFunction.Exit:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Enter a valid input!");
                        continue;
                }
                break;
            }
        }
        public static void PatientSignUp()
        {
            while (true)
            {
                string a = VerifyPhone();
                bool phoneVerify = AuthManager<User>.AuthMInstance.CheckPhoneNoExists(a);
                if (phoneVerify == true)
                {
                    Console.WriteLine("Phone Number already exists !, you can directly log in");
                    continue;
                }
                RegisterUI.UserRegister(Role.Patient.ToString(),a);

                Helpers();
                PatientUI.choose(a);
            }
        }
        public static string VerifyPhone()
        {
            var pn = "";
            while (true)
            {
                Console.WriteLine("Enter phone number in the format (+91**********) : ");
                pn = Console.ReadLine();
                var ans = false;

                ans = AuthManager<Patient>.AuthMInstance.PhoneNoVerify(pn);

                if (ans == false)
                {
                    Console.WriteLine("Invalid credentials");
                    //LoginUI.UserLogin();
                    continue;
                }

                break;
            }
            return pn;
        }
        public static void AdminLogin(string role)
        {
            Console.WriteLine("Enter vaccination center : ");
            string vaccineCenter = Console.ReadLine();
            if (AuthManager<User>.AuthMInstance.VerifyVaccineCenter(vaccineCenter) == true)
                LocalAdminUI.choose(vaccineCenter);
            else
            {       Console.WriteLine("Invalid Details ! ");
                MainFunction();
               
        }
        }
        
        public static void Helpers()
        {
            while (true) {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Press 1 : login ");
                Console.WriteLine("Press 2 : Exit");
                int input = Convert.ToInt32(Console.ReadLine());
                Console.ResetColor();
                switch (input)
                {
                    case (int)StartFunction.Login:
                        List<string> data=LoginUI.UserLogin();
                        if (string.IsNullOrWhiteSpace(data[0]) || data[0] != "Patient")
                        {
                            Console.WriteLine("Invalid Credentials");
                            continue;
                        }
                        
                        break;
                    case (int)StartFunction.Exit:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        continue;
                }
                break;
            }
        }
    }
}
