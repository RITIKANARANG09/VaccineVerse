using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var password = Console.ReadLine();
                Console.WriteLine("Enter phone number : ");
                var phnNo= Console.ReadLine();
                string role = AuthManager<User>.AuthMInstance.Login(username, password, phnNo);
                if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password) || String.IsNullOrWhiteSpace(phnNo) || role == "Wrong input")
                {
                    Console.WriteLine("Invalid credentials");
                    continue;
                }
                LoginCredentials.Add(role); LoginCredentials.Add(phnNo);
                return LoginCredentials;
            }
        }
        
    }
}
