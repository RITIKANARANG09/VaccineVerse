

namespace Project
{
    internal class LoginUI
    {
        public static User UserLogin()
        {
            while (true)
            {
                
                Console.WriteLine("\n" + Message.printPhoneNoFormat);
                var phnNo = Console.ReadLine();
                Console.WriteLine(Message.inputPassword);
                var password = HideP.HidePassword();
               
                User user= AuthManager<User>.AuthMInstance.Login(password, phnNo);
                if (String.IsNullOrWhiteSpace(password) || String.IsNullOrWhiteSpace(phnNo) || user==null)
                {
                    Console.WriteLine("\n"+Message.printInvalidChoice);
                    continue;
                }
                return user;
            }
        }
        
    }
}
