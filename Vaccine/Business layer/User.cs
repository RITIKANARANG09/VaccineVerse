using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public enum Role
    {
        GlobalAdmin,
        Admin,
        Patient
    }
    public  class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string phoneNo { get; set; }
        public Role role;
    }
}
