
namespace Project
{
    public class RegisterUI
    {
        /*public static void SetVaccineCenterDetails(string vaccineCenterInput, string vaccineCenterAddress)
        {

            while (true)
            {
                Console.WriteLine(Message.inputVaccinationCenterName);
                vaccineCenterInput = Console.ReadLine();
                Console.WriteLine(Message.inputVaccinationCenterAddress);
                vaccineCenterAddress = Console.ReadLine();
                bool isPresent = AuthManager<User>.AuthMInstance.VaccineCenterAlreadyPresent(vaccineCenterInput, vaccineCenterAddress);
                if (isPresent == true)
                {
                    Console.WriteLine(Message.printVaccineCenterPresentAlready);

                    continue;
                }
                break;
            }

        }*/
        public static  VaccineCenter GetNewVaccineCenter(string username)
        {
            string vaccineCenterInput;
            string vaccineCenterAddress;
            while (true)
            {
                Console.WriteLine(Message.inputVaccinationCenterName);
                vaccineCenterInput = Console.ReadLine();
                Console.WriteLine(Message.inputVaccinationCenterAddress);
                vaccineCenterAddress = Console.ReadLine();
                bool isPresent = AuthManager<User>.AuthMInstance.VaccineCenterAlreadyPresent(vaccineCenterInput, vaccineCenterAddress);
                if (isPresent == true)
                {
                    Console.WriteLine(Message.printVaccineCenterPresentAlready);

                    continue;
                }
                break;
            }

            var vaccineCenter = new VaccineCenter()
            {
                Address = vaccineCenterAddress,
                VcName = vaccineCenterInput,
                VcId = ++VaccineCenter.vCenterIDInc,
                LaName = username
            };
            return vaccineCenter;
        }
        public static void UserRegister(string role, string phoneNo)
        {
            while (true)
            {
                Console.WriteLine(Message.inputUsername);
                var username = Console.ReadLine();
                var isUsernameExists = AuthManager<User>.AuthMInstance.ValidateUser(username);
                if (isUsernameExists) { Console.WriteLine(Message.printUserExitsAlready); continue; }
                Console.WriteLine(Message.inputPassword);
                var password = HideP.HidePassword();

                var newUser = new User
                {
                    Username = username,
                    Password = password,
                    PhoneNo = phoneNo
                };
                if (role.Equals(Role.Admin.ToString()))
                {
                    newUser.RoleOfUser = Role.Admin;
                    VaccineCenter vaccineCenter=GetNewVaccineCenter(username);
                        AuthManager<User>.AuthMInstance.Register(newUser,vaccineCenter);
                }
                else {
                    newUser.RoleOfUser = Role.Patient;
                    AuthManager<User>.AuthMInstance.Register(newUser);
                    break;
                }
                break;
            }
        }
       
   


    }

}
