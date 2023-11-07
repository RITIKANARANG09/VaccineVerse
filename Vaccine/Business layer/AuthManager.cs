
using Vaccine.Model;


namespace Project
{
    public class AuthManager<T> where T : User, new()
    {
        List<User> UsersList = ConnectToDataBase.DataBaseInstance.readUsersList();
        List<VaccineCenter> VaccineCenterList = ConnectToDataBase.DataBaseInstance.readVaccineCenterList();
        private static AuthManager<T> authMInstance;
        public static AuthManager<T> AuthMInstance
        {
            get
            {
                if (authMInstance == null)
                {
                    authMInstance = new AuthManager<T>();
                }
                return authMInstance;
            }

        }
        public bool CheckUserExists(string username)
        {
            User user = UsersList.Find(user => user.Username == username);
            if (user == null) return false;
            else return true;
        }
        public User Login( string Password, string pn)
        {
            
                var matchedUser = UsersList.FirstOrDefault(user=> user.Password == Password && user.phoneNo == pn);

                if (matchedUser != null)
                {
                
                    return matchedUser;
                }
                return null;
        }
        public bool CheckPhoneNoExists(string phoneNo)
        {
           

            User user = UsersList.Find(u => u.phoneNo == phoneNo);
            if (user != null)
            {
                return true;
            }

            return false;
        }

        public void Register(string username, string phoneNo, string password, Role roleInput, string vaccinationCenterName = null)
        {

            var newUser = new T
            {
                Username = username,
                Password = password,
                phoneNo = phoneNo,
                role = (Role)roleInput
            };
     
            UserDataBase.UserInstance.AddUser(newUser);
            if (roleInput == Role.Admin)
            {
                VaccineCenterController centerControllerObj=new VaccineCenterController();
                centerControllerObj.AddNewVaccinationCenter(newUser, vaccinationCenterName);
            }

        }
        public bool VerifyPhone(string pn)
        {

            bool ans = RegexValid.PhoneNoVerify(pn);
            return ans;
        }
        public VaccineCenter VerifyVaccineCenter(Admin admin,string vaccineCenter)
        {
            VaccineCenter center = VaccineCenterList.Find(v => v.VcName == vaccineCenter  &&v.LaName==admin.Username);
            if (center != null)
            {
                return center;
            }
            return null;
        }
        public bool VaccineCenterAlreadyPresent(string vaccineCenter)
        {
            
                var isPresentVCenter = VaccineCenterList.Find(v => v.VcName==vaccineCenter);
                if (isPresentVCenter != null)
                    return true;
                return false;
        }

        public List<VaccineCenter> ViewAdmins()
        {
                return VaccineCenterList;
        }

    }
}

