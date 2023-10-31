
using Vaccine.Model;

namespace Project
{
    internal class LoginUI
    {
        public static   List<string> UserLogin()
        {
            List<string> LoginCredentials=new List<string>();
            while (true)
            {
                Console.WriteLine("Enter username : ");
                var username = Console.ReadLine();
                Console.WriteLine("Enter password : ");
                var password = HideP.HidePassword();
                Console.WriteLine("\nEnter phone number : ");
                var phnNo= Console.ReadLine();
                string role = AuthManager<User>.AuthMInstance.Login(username, password, phnNo);
                if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password) || String.IsNullOrWhiteSpace(phnNo) || role == "Wrong input")
                {
                    Console.WriteLine("Invalid credentials");
                    continue;
                }
                else if(role==null)
                 continue;
                LoginCredentials.Add(role); LoginCredentials.Add(phnNo);
                return LoginCredentials;
            }
        }
        
    }
}
