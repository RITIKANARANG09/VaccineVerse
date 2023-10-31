
using Vaccine.Model;

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
             Choose.GlobalAdminUIChoose();
                GlobalAdminChoose input;

             input = (GlobalAdminChoose)Validation.IntValidate();
                Console.ResetColor();
                
                switch (input)
                {
                    case GlobalAdminChoose.AddAdmin:
                        addAdminChecker();
                        continue ;
                    case GlobalAdminChoose.AddVaccine:
                        VaccineUI.AddVaccineByGA();
                        helper();
                        continue;
                    case GlobalAdminChoose.ViewVaccines:
                        VaccineUI.ViewVaccine();
                        helper();
                        continue;
                    case GlobalAdminChoose.ViewAdmin:
                        viewAdmins();
                        helper();
                        continue;
                    case GlobalAdminChoose.Exit:
                        Environment.Exit(0);
                        break;
                    default:
                        ExceptionController.NotValid();
                        continue;
                }
                break;
            }
        }
        public static void addAdminChecker()
        {
            var input = "";
            while (true) {
                Console.WriteLine(Message.inputPhoneNo);
                input = Console.ReadLine();
                if (AuthManager<User>.AuthMInstance.CheckPhoneNoExists(input!) || AuthManager<User>.AuthMInstance.VerifyPhone(input) == "Invalid Credentials")
                {
                    Console.WriteLine("Phone number either exists or is not correct");
                    continue;
                }
            VerifyVaccineCenterOption: Console.WriteLine(Message.inputVaccinationCenter);
                var vaccineCenterInput = Console.ReadLine();
                    bool isPresent = AuthManager<User>.AuthMInstance.VaccineCenterAlreadyPresent(vaccineCenterInput);
                if(isPresent==true) { Console.WriteLine("Vaccine center already present !"); goto VerifyVaccineCenterOption; }
                break;
            }

            RegisterUI.UserRegister(Role.Admin.ToString(),input);
            helper();
        }
        public static void viewAdmins()
        {
            var vaccineCenterDetails=AuthManager<User>.AuthMInstance.ViewAdmins();
            var input=vaccineCenterDetails.Distinct().ToList();
            if(input==null)
            {
                Choose.GlobalAdminUIChoose();
            }
           Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("LAdmins     VaccineCenter\n");
            Console.ResetColor();
            foreach(var VaccineCenter in input)
            {
                Console.WriteLine(VaccineCenter.LaName+ " : "+VaccineCenter.VcName);
            }
        }
        
        public static void helper()
        {

            HelperOption:  Console.ForegroundColor = ConsoleColor.Blue;
            Choose.HelperChoose();
            Helper input = (Helper)Validation.IntValidate();
                Console.ResetColor();
                if (input == Helper.Choose) 
                choose();
                else if(input==Helper.Exit)
                    Environment.Exit(0);
                else
            {
                ExceptionController.NotValid();
                goto HelperOption;
            }

           
        }
    }
}
