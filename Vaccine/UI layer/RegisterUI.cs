using Vaccine.Model;

namespace Project
{
    public class RegisterUI
    {
        public static void UserRegister(string role,string phoneNo,string vcname=null)
        {
            while (true)
            {
                    Console.WriteLine(Message.inputUsername);
                    var username = Console.ReadLine();
                var isUsernameExists = AuthManager<User>.AuthMInstance.CheckUserExists(username);
                if(isUsernameExists) { Console.WriteLine(Message.printUserExitsAlready);continue; }
                Console.WriteLine(Message.inputPassword);
                    var password = HideP.HidePassword();
                    if (role == Role.Admin.ToString())
                    {
                       
                        AuthManager<User>.AuthMInstance.Register(username, phoneNo, password, Role.Admin, vcname);
                    }
                    else { 
                        AuthManager<User>.AuthMInstance.Register(username, phoneNo, password, Role.Patient);
                    break;
                }
                break;
            }
           
        }
        
    }
}
