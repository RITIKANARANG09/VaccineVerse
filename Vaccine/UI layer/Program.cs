
using Vaccine.Model;

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
        
        public static void Main(String[]args)
        {
            try
            {
                MainFunction();
            }
            catch(Exception ex) {
            
            }
        }
        public static void MainFunction()
        {
            while (true)
            {
            mainOptions: Choose.MainFunctionChoose();
                StartFunction choice;
                try
                {
                     choice = (StartFunction)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    ExceptionController.OnlyNumeric();
                    goto mainOptions;
                }
                Console.ResetColor();
                switch (choice)
                {
                    case StartFunction.Login:

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

                    case StartFunction.Signup:
                        PatientSignUp();
                        break;

                    case StartFunction.ViewVaccines:
                        VaccineUI.ViewVaccine();
                        continue;
                    case StartFunction.Exit:
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
                if (AuthManager<User>.AuthMInstance.CheckPhoneNoExists(a)==true) continue;
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
                if (RegexValid.PhoneNoVerify(pn) == false) continue;
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
            {       
                MainFunction();
               
        }
        }
        
        public static void Helpers()
        {
            while (true) {
                Console.ForegroundColor = ConsoleColor.Blue;
                helperOptions:  Console.WriteLine("Press 1 : login ");
                Console.WriteLine("Press 2 : Exit");
                int input;
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    ExceptionController.OnlyNumeric();
                    goto helperOptions;
                }
               
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
