
namespace Project
{

    class Proj
    {
    
        
        public static void Main()
        {
            try
            {
                MainFunction();
            }
            catch(Exception ex)
            {
                Errors.SomethingWentWrong();
                ExceptionController.LogException(ex, "An unknown error occured");
            }
        }
        public static void MainFunction()
        {
            
            
            while (true)
            {
            Choose.MainFunctionChoose();

                StartFunction choice=(StartFunction)Validation.IntValidate();
                Console.ResetColor();
                switch (choice)
                {
                    case StartFunction.Login:

                        User user = LoginUI.UserLogin();
                        if (user.RoleOfUser.Equals(Role.Admin))
                        {
                            Console.WriteLine("\n---------------------------- Admin --------------------------------");
                            var adminObj = new LocalAdminUI();
                            var admin = new Admin() { Username = user.Username, Password = user.Password, PhoneNo = user.PhoneNo, RoleOfUser = user.RoleOfUser };
                            adminObj.AdminLogin(admin);
                            continue;
                        }
                        else if ( user.RoleOfUser.Equals(Role.GlobalAdmin))
                        {
                            var globalAdminObj = new GlobalAdminUI();
                            var globalAdmin = new GlobalAdmin() { Username=user.Username, 
                            Password=user.Password,PhoneNo=user.PhoneNo,RoleOfUser=user.RoleOfUser};
                            Console.WriteLine("\n--------------------- Global Admin ------------------------------");
                            globalAdminObj.choose(globalAdmin);
                        }
                         else if (user.RoleOfUser.Equals(Role.Patient))
                        {
                        var patientObj = new PatientUI();
                            var patient = new Patient() { Username=user.Username,Password=user.Password,PhoneNo=user.PhoneNo,RoleOfUser=user.RoleOfUser};
                            Console.WriteLine("\n---------------------------- Patient --------------------------------");
                            patientObj.PatientUIChoose(patient);
                        }
                        else
                        {
                            Console.WriteLine(Message.printInvalidChoice);
                            continue;
                        }
                        break;

                    case StartFunction.Signup:
                        PatientSignUp();
                        continue ;

                    case StartFunction.ViewVaccines:
                        var globalAdminUI = new GlobalAdminUI();
                        globalAdminUI.GloballyViewVaccine();
                        continue;
                   
                    case StartFunction.Exit:
                        Environment.Exit(0);
                        break;
                    
                    default:
                        Console.WriteLine(Message.printInvalidChoice);
                        continue;
                }
                break;
            }
        }
        public static void PatientSignUp()
        {
            while (true)
            {
                string phoneNoCheck = VerifyPhone();
                if (AuthManager<User>.AuthMInstance.FindPhoneNumber(phoneNoCheck))
                { Console.WriteLine(Message.printPhoneNoExists);
                    continue; }

                RegisterUI.UserRegister(Role.Patient.ToString(), phoneNoCheck);
                Console.WriteLine(Message.printUserAdded);
                
                Helpers();
                break;
            }
        }
        public static string VerifyPhone()
        {
            string phoneNumber;
            while (true)
            {
                Console.WriteLine(Message.printPhoneNoFormat);
                phoneNumber = Console.ReadLine();
               
                if (RegexValid.PhoneNoVerify(phoneNumber) == false) continue;
                break;
            }
            return phoneNumber;
        }


        public static void Helpers()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Choose.PatientHelper();
                PatientChooseHelper input = (PatientChooseHelper)Validation.IntValidate();
                Console.ResetColor();
                switch (input)
                {
                    case PatientChooseHelper.Login:
                       // goto Label;
                        User user = LoginUI.UserLogin();
                        if (string.IsNullOrWhiteSpace(user.RoleOfUser.ToString()) || user.RoleOfUser.ToString() != "Patient")
                        {
                            Console.WriteLine(Message.printInvalidChoice);
                            continue;
                        }

                        var patientObj = new PatientUI();
                        var patient = new Patient() { Username = user.Username, Password = user.Password, PhoneNo = user.PhoneNo, RoleOfUser = user.RoleOfUser };
                        Console.WriteLine("---------------------------- Patient --------------------------------");
                        patientObj.PatientUIChoose(patient);
                        break;

                    case PatientChooseHelper.Exit:
                        Environment.Exit(0);
                        break;
                    
                    default:
                        Console.WriteLine(Message.printInvalidChoice);
                        continue;
                }
                break;
            }
        }
    }
}
