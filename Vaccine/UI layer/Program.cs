
using Vaccine.Model;

namespace Project
{

    class Proj
    {
    
        
        public static void Main(String[]args)
        {
            try
            {
                MainFunction();
            }
            catch
            {
                ExceptionController.SomethingWentWrong();
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
                        if (user.role == Role.Admin)
                        {
                            Console.WriteLine("\n---------------------------- Admin --------------------------------");
                            LocalAdminUI adminObj = new LocalAdminUI();
                            Admin admin = new Admin() { Username = user.Username, Password = user.Password, phoneNo = user.phoneNo, role = user.role };
                            adminObj.AdminLogin(admin);
                            continue;
                        }
                        else if ( user.role== Role.GlobalAdmin)
                        {
                            GlobalAdminUI globalAdminObj = new GlobalAdminUI();
                            GlobalAdmin globalAdmin = new GlobalAdmin() { Username=user.Username, 
                            Password=user.Password,phoneNo=user.phoneNo,role=user.role};
                            Console.WriteLine("\n--------------------- Global Admin ------------------------------");
                            globalAdminObj.choose(globalAdmin);
                        }
                         else if (user.role == Role.Patient)
                        {
                        PatientUI patientObj = new PatientUI();
                            Patient patient = new Patient() { Username=user.Username,Password=user.Password,phoneNo=user.phoneNo,role=user.role};
                            Console.WriteLine("\n---------------------------- Patient --------------------------------");
                            patientObj.choose(patient);
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
                        GlobalAdminUI globalAdminUI = new GlobalAdminUI();
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
                if (AuthManager<User>.AuthMInstance.CheckPhoneNoExists(phoneNoCheck))
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
            var pn = "";
            while (true)
            {
                Console.WriteLine(Message.printPhoneNoFormat);
                pn = Console.ReadLine();
                var ans = false;
                if (RegexValid.PhoneNoVerify(pn) == false) continue;
                break;
            }
            return pn;
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
                        if (string.IsNullOrWhiteSpace(user.role.ToString()) || user.role.ToString() != "Patient")
                        {
                            Console.WriteLine(Message.printInvalidChoice);
                            continue;
                        }

                        PatientUI patientObj = new PatientUI();
                        Patient patient = new Patient() { Username = user.Username, Password = user.Password, phoneNo = user.phoneNo, role = user.role };
                        Console.WriteLine("---------------------------- Patient --------------------------------");
                        patientObj.choose(patient);
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
