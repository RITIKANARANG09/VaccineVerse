

namespace Vaccine.Model
{
    public enum Role
    {
        GlobalAdmin,
        Admin,
        Patient
    }
    public class User
    {


        public string Username { get; set; }
        //public string Name { get; set; }
        public string Password { get; set; }
        public string phoneNo { get; set; }
        public Role role;

    }
}
