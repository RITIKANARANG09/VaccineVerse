
namespace Project
{
    public class AuthManager<T> where T : User, new()
    {
       
        private static AuthManager<T> _authMInstance;
        public static AuthManager<T> AuthMInstance
        {
            get
            {
                if (_authMInstance == null)
                {
                    _authMInstance = new AuthManager<T>();
                }
                return _authMInstance;
            }

        }
        public bool ValidateUser(string username)
        {
            User user = UserDataBase.UserInstance.UsersList.Find(user => user.Username.ToLower().Equals(username.ToLower()));
            if (user == null) return false;
            else return true;
        }
        public User Login( string Password, string pn)
        {
            
                var matchedUser = UserDataBase.UserInstance.UsersList.FirstOrDefault(user=> user.Password.Equals(Password) && user.PhoneNo.Equals(pn));

                if (matchedUser != null)
                {
                
                    return matchedUser;
                }
                return null;
        }
        public bool ValidateAdmin(string phoneNumber)
        {

            var isPhoneNoValid = VerifyPhone(phoneNumber);
            var isPhoneNoExist = FindPhoneNumber(phoneNumber!);

            if (isPhoneNoExist == true || isPhoneNoValid == false)
            {
                return false;
            }


            return true;

        }
        public Appointment FindAppointments(DateTime date)
        {
            var AppointmentList=AppointmentDataBase.AppointmentInstance.AppointmentList;
            return AppointmentList.Find(appointment => appointment.Date == date);
        }
        public Vaccine FindVaccine(string vaccineName,VaccineCenter vaccineCenter=null)
        {
            if(vaccineCenter==null)
            {
                List<Vaccine>VaccineList=VaccineDataBase.VaccineInstance.vaccineList;
                return VaccineList.Find(vaccine => vaccine.VName.ToLower().Equals(vaccineName.ToLower()));
            }
            else
            {
                return vaccineCenter.vaccines.Find(vaccine=>vaccine.VName.ToLower().Equals(vaccineName.ToLower()));
            }
        }
        public bool FindPhoneNumber(string phoneNo)
        {
           

            User user = UserDataBase.UserInstance.UsersList.Find(u => u.PhoneNo.ToLower().Equals(phoneNo.ToLower()));
            if (user != null)
            {
                return true;
            }

            return false;
        }

        public bool Register(User newUser,VaccineCenter vaccineCenterObject=null)
        {

            UserDataBase.UserInstance.AddUser(newUser);
            if (newUser.RoleOfUser.Equals(Role.Admin))
            {
                var centerControllerObj=new VaccineCenterController();
                return centerControllerObj.Add(vaccineCenterObject);
            }
            return true;
        }
        public bool VerifyPhone(string pn)
        {

            bool ans = RegexValid.PhoneNoVerify(pn);
            return ans;
        }
        public VaccineCenter ValidateVaccineCenter(Admin admin,string vaccineCenterName,string vaccineCenterAddress)
        {
            VaccineCenter vaccineCenter = VaccineCenterDataBase.VaccineCenterInstance.VaccineCenterList.Find(v => v.VcName.ToLower().Equals(vaccineCenterName.ToLower()) && v.Address.ToLower().Equals(vaccineCenterAddress.ToLower())  &&v.LaName.ToLower().Equals(admin.Username.ToLower()));
            if (vaccineCenter != null)
            {
                return vaccineCenter;
            }
            return null;
        }
        public bool VaccineCenterAlreadyPresent(string vaccineCenter,string address)
        {
            
                var isPresentVCenter = VaccineCenterDataBase.VaccineCenterInstance.VaccineCenterList.Find(v => v.VcName.ToLower().Equals(vaccineCenter.ToLower()) && v.Address.ToLower().Equals(address.ToLower()));
                if (isPresentVCenter != null)
                    return true;
                return false;
        }

      

    }
}

