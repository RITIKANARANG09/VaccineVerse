using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class RegisterUI
    {
        public static void UserRegister(string role,string a,string vcname=null)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Username:");
                    var username = Console.ReadLine();

                    Console.WriteLine("Enter Password:");
                    var password = Console.ReadLine();
                    if (role == Role.Admin.ToString())
                    {
                       
                        AuthManager<User>.AuthMInstance.Register(username, a, password, Role.Admin, vcname);
                    }
                    else { 
                        AuthManager<User>.AuthMInstance.Register(username, a, password, Role.Patient);
                }
                    
                }
                catch { 
                
                }
                break;
            }
           
        }
        
    }
}
