using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    enum GlobalAdminChoose
    {
        AddAdmin=1,
        AddVaccine,
        ViewVaccines,
        ViewAdmin,
        Exit,
          
    }
    public class GlobalAdminUI : User
    {
        public static void choose()
        {
            

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Choose any one : ");
                Console.WriteLine("1. Add admin");
                Console.WriteLine("2. Add vaccine");
                Console.WriteLine("3. View vaccines");
                Console.WriteLine("4. View Admins");
                Console.WriteLine("5. Exit");
                var input = Convert.ToInt32(Console.ReadLine());
                Console.ResetColor();
                
                switch (input)
                {
                    case (int)GlobalAdminChoose.AddAdmin:
                        addAdminChecker();
                        continue ;
                    case (int)GlobalAdminChoose.AddVaccine:
                        VaccineUI.AddVaccineByGA();
                        helper();
                        continue;
                    case (int)GlobalAdminChoose.ViewVaccines:
                        VaccineUI.ViewVaccine();
                        helper();
                        continue;
                    case (int)GlobalAdminChoose.ViewAdmin:
                        viewAdmins();
                        helper();
                        continue;
                    case (int)GlobalAdminChoose.Exit:
                        Environment.Exit(0);
                        break;
                }
                break;
            }
        }
        public static void addAdminChecker()
        {
            var input = "";
            while (true) { 
            Console.Write("Enter phone Number of Admin : ");
            input = Console.ReadLine();
                Console.Write("\nEnter vaccination Center : ");
                var vaccineCenterInput = Console.ReadLine();
                VaccineCenter: try
                {
                    bool isPresent = AuthManager<User>.AuthMInstance.VaccineCenterAlreadyPresent(vaccineCenterInput);
                    if (isPresent)
                    {
                        Console.WriteLine("Vaccine Center already present !");
                        goto VaccineCenter;
                        }
                }
                catch (Exception e) { Console.WriteLine("Error occured, please try again"); }
                if (AuthManager<User>.AuthMInstance.CheckPhoneNoExists(input) || AuthManager<User>.AuthMInstance.VerifyPhone(input) == "Invalid Credentials")
            { Console.WriteLine("Phone number either exists or is not correct");
                continue; }
                break;
            }

            RegisterUI.UserRegister(Role.Admin.ToString(),input);
            helper();
        }
        public static void viewAdmins()
        {
            var vaccineCenterDetails=AuthManager<User>.AuthMInstance.ViewAdmins();
            var input=vaccineCenterDetails.Distinct().ToList();
           Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("LAdmins     VaccineCenter\n");
            Console.ResetColor();
            foreach(var VaccineCenter in input)
            {
                Console.WriteLine(VaccineCenter.LaName+ " : "+VaccineCenter.VcName);
            }
        }
        private enum Helper
        {
            Choose=1
        }
        public static void helper()
        {
          chooseH:  Console.ForegroundColor= ConsoleColor.Blue;
            Console.WriteLine("Choose 1 to go back");
            Console.WriteLine("2 for exit ");
            try
            {
                int input = Convert.ToInt32(Console.ReadLine());
                Console.ResetColor();
                if (input == (int)Helper.Choose) choose();
                else
                    Environment.Exit(0);
            }
            catch
            {
                Console.WriteLine("Invalid input");
                goto chooseH;
            }
        }
    }
}
