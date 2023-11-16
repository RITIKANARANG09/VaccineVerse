

namespace Project
{
    
    public class GlobalAdminUI : User
    {
        IVaccineControllerForGlobalAdmin vaccineControllerObj = new VaccineController();
        LocalAdminController localAdminControllerObj = new LocalAdminController();

        public void choose(GlobalAdmin globalAdminObj)
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
                        AddAdmin(globalAdminObj);
                        continue ;

                    case GlobalAdminChoose.AddVaccine:  
                        GloballyAddVaccine();
                        Helper(globalAdminObj);
                        continue;

                    case GlobalAdminChoose.ViewVaccines:
                        GloballyViewVaccine();
                        Helper(globalAdminObj);
                        continue;
                    
                    case GlobalAdminChoose.ViewAdmins:
                        ViewAdmins();
                        Helper(globalAdminObj);
                        continue;
                    
                    case GlobalAdminChoose.Exit:
                        Environment.Exit(0);
                        break;
                    
                    default:
                        Errors.NotValid();
                        continue;
                }
                break;
            }
        }
        void GloballyAddVaccine()
        {
            while (true)
            {
                Vaccine newVaccine=InputVaccineDetails();
                
                if (vaccineControllerObj.AddVaccineGlobally(newVaccine) == false)
                {       
                    Console.WriteLine(Message.printVaccineNotAdded);
                    continue;
                }
                else
                {
                    Console.WriteLine(Message.printVaccineAdded);
                    break;
                }
            }
        }
        Vaccine InputVaccineDetails()
        {
            while (true)
            {
                Console.Write(Message.inputVaccineName);
                 GloballyViewVaccine();
                var vaccineName = Console.ReadLine();
                var vaccineObj = AuthManager<GlobalAdmin>.AuthMInstance.FindVaccine(vaccineName);
                if (vaccineObj!=null)
                {
                    Console.WriteLine(Message.printVAaccinePresentAlready);
                    continue;
                }

                Console.WriteLine(Message.inputMinAgeForVaccine);
                int minAge = Validation.IntValidate();

                Console.WriteLine(Message.inputMaxAgeForVaccine);
                var maxAge = Validation.IntValidate();

                var newVaccine = new Vaccine(vaccineName, minAge, maxAge);
                return newVaccine;
            }
        }

        public List<Vaccine> GloballyViewVaccine()
        {
            List<Vaccine> VaccineList =vaccineControllerObj.ViewVaccinesGlobally();
            Console.WriteLine(Message.printGloballyAvailableVaccine);
            foreach(var vaccine in VaccineList)
            {
                Console.WriteLine(vaccine.VName);
            }
            return VaccineList;
        }
        void AddAdmin(GlobalAdmin globalAdminObj)
        {
            string phoneNumber;
            while (true)
            {
                Console.WriteLine(Message.printPhoneNoFormat);
                phoneNumber = Console.ReadLine();

                if(AuthManager<Admin>.AuthMInstance.ValidateAdmin(phoneNumber)==false)
                {
                    Console.WriteLine(Message.printInvalidPhoneDetails);
                    continue;
                }
                break;
            }
           
            RegisterUI.UserRegister(Role.Admin.ToString(), phoneNumber);
            
            Helper(globalAdminObj);
        }
        
       
         void ViewAdmins()
        {
            List<VaccineCenter> vaccineCenterList=localAdminControllerObj.ViewAdmins();
      
           Console.ForegroundColor = ConsoleColor.Gray;
            foreach(var VaccineCenter in vaccineCenterList)
            {
                Console.WriteLine(VaccineCenter.LaName);
                Console.WriteLine(VaccineCenter.VcName+"\n");
            }
        }
        
        void Helper(GlobalAdmin globalAdminObj)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Choose.HelperChoose();
                Helper input = (Helper)Validation.IntValidate();
                Console.ResetColor();
                
                if (input.Equals(Project.Helper.Choose))
                    choose(globalAdminObj);
                
                else if (input.Equals(Project.Helper.Exit))
                    Environment.Exit(0);
                
                else
                {
                    Errors.NotValid();
                    continue;
                }
                break;
            }
        }
    }
}
