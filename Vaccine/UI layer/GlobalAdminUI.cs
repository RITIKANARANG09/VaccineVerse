
using Vaccine.Model;

namespace Project
{
    
    public class GlobalAdminUI : User
    {
        VaccineController vaccineControllerObj = new VaccineController();
        public  void choose(GlobalAdmin globalAdminObj)
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
                        addAdminChecker(globalAdminObj);
                        continue ;

                    case GlobalAdminChoose.AddVaccine:  
                        GloballyAddVaccine();
                        helper(globalAdminObj);
                        continue;

                    case GlobalAdminChoose.ViewVaccines:
                        GloballyViewVaccine();
                        helper(globalAdminObj);
                        continue;
                    
                    case GlobalAdminChoose.ViewAdmin:
                        viewAdmins();
                        helper(globalAdminObj);
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
        public  void GloballyAddVaccine()
        {
            while (true)
            {

                Console.Write(Message.inputAddVaccineGlobally);
                List<Vaccine> vaccinesAvailable = GloballyViewVaccine();
                var addVaccine = Console.ReadLine();
                
                if (vaccinesAvailable.Any(vaccine => vaccine.vname==addVaccine))
                {
                    Console.WriteLine(Message.printVAaccinePresentAlready);
                    continue;
                }

                Console.WriteLine(Message.inputMinAgeForVaccine);
                int minAge = Validation.IntValidate();
                
                Console.WriteLine(Message.inputMaxAgeForVaccine);
                var maxAge = Validation.IntValidate();
               
                Vaccine newVaccine = new Vaccine(addVaccine, minAge, maxAge);
                if (vaccineControllerObj.AddVaccineGlobally(newVaccine) == false)
                {       Console.WriteLine(Message.printVaccineNotAdded);
                continue;
                }
                else
                {
                    Console.WriteLine(Message.printVaccineAdded);
                    break;
                }
            }
        }
        public List<Vaccine> GloballyViewVaccine()
        {
            List<Vaccine> vaccineList =vaccineControllerObj.ViewVaccinesGlobally();
            Console.WriteLine(Message.printGloballyAvailableVaccine);
            foreach(var vaccine in vaccineList)
            {
                Console.WriteLine(vaccine.vname);
            }
            return vaccineList;
        }
        public  void addAdminChecker(GlobalAdmin globalAdminObj)
        {
            var input = "";var  vaccineCenterInput="";
            while (true) {
                Console.WriteLine(Message.printPhoneNoFormat);
                input = Console.ReadLine();

                var phoneVerify = AuthManager<User>.AuthMInstance.VerifyPhone(input);
                var isPhoneNoExist = AuthManager<User>.AuthMInstance.CheckPhoneNoExists(input!);
                
                if ( isPhoneNoExist==true|| phoneVerify==false)
                {
                    Console.WriteLine(Message.printInvalidPhoneDetails);
                    continue;
                }
                VerifyVaccineCenterOption: Console.WriteLine(Message.inputVaccinationCenter);
                vaccineCenterInput = Console.ReadLine();
                bool isPresent = AuthManager<User>.AuthMInstance.VaccineCenterAlreadyPresent(vaccineCenterInput);
                if(isPresent==true) { Console.WriteLine(Message.printVaccineCenterPresentAlready); 
                    goto VerifyVaccineCenterOption; }
                break;
            }

            RegisterUI.UserRegister
                (Role.Admin.ToString(),input,vaccineCenterInput);
            helper(globalAdminObj);
        }
        public   void viewAdmins()
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
                Console.WriteLine(VaccineCenter.LaName+ "     :     "+VaccineCenter.VcName);
            }
        }
        
        public  void helper(GlobalAdmin globalAdminObj)
        {

            HelperOption:  Console.ForegroundColor = ConsoleColor.Blue;
            Choose.HelperChoose();
            Helper input = (Helper)Validation.IntValidate();
                Console.ResetColor();
                if (input == Helper.Choose) 
                choose(globalAdminObj);
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
