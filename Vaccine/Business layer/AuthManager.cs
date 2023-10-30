﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project
{
    public  class AuthManager<T> where T : User, new()
    {       
        private static AuthManager<T> authMInstance;
        public static AuthManager<T> AuthMInstance
        {
            get
            {
                if(authMInstance == null)
                {
                    authMInstance = new AuthManager<T>();
                }
                return authMInstance;
            }
            
        }
        public  string Login(string Username, string Password,string pn)
        {
            List<User> users = DB.DbInstance.UserRead();
            //User user = users.Find(u => u.Username == Username && u.Password == Password);
            var matchingUser = users.FirstOrDefault(user => user.Username == Username && user.Password == Password && user.phoneNo==pn);

            if (matchingUser != null)
            {
                return matchingUser.role.ToString();
            }
            return "Wrong input";
        }
        public  bool PhoneNoVerify(string phoneNo)
        {
            List<User> users = DB.DbInstance.UserRead();
            //User user = users.Find(u => u.phoneNo == phoneNo );
            string pattern = @"^(?:\+91)?[6-9][0-9]{9}$";
            if (Regex.IsMatch(phoneNo, pattern))
            {
                return true;
            }
            return false;
        }
        public bool CheckPhoneNoExists(string phoneNo)
        {
            List<User> users = DB.DbInstance.UserRead();
            User user = users.Find(u => u.phoneNo == phoneNo);
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public  void Register( string username, string phoneNo, string password, Role roleInput, string vaccinationCenterName= null)
        {
          
                var newUser = new T
                {
                    Username = username,
                    Password = password,
                    phoneNo = phoneNo,
                    role = (Role)roleInput
                };
            DB.DbInstance.AddUser(newUser);
            if (roleInput == Role.Admin)
            {
                VaccineCenter.AddNewVaccinationCenter(newUser, vaccinationCenterName);
            }
            

            
            
        }
        public string VerifyPhone(string pn)
        {

            bool ans = AuthManager<Patient>.AuthMInstance.PhoneNoVerify(pn);
            if (ans == false)
            {
                return "Invalid Credentials";
               
            }
            return null;
        }
        public bool VerifyVaccineCenter(string vaccineCenter)
        {
            List<VaccineCenter> vc = DB.DbInstance.VaccineCenterRead();
            VaccineCenter center = vc.Find(v => v.VcName == vaccineCenter && v.role == Role.Admin.ToString());
            if (center != null)
            {
                return true;
            }
            return false;
        }
        public bool VaccineCenterAlreadyPresent(string vaccineCenter)
        {
            var vaccineCenterDetails = DB.DbInstance.VaccineCenterRead();
            var isPresentVCenter=vaccineCenterDetails.Find(v=>v.Equals(vaccineCenter));
            if (isPresentVCenter != null)
                return false;
            return true;
        }

       public List<VaccineCenter> ViewAdmins()
        {
            List<VaccineCenter> admins = new List<VaccineCenter>();
            var ReadAdmin = DB.DbInstance.VaccineCenterRead();
            return ReadAdmin;
        }
    }
}