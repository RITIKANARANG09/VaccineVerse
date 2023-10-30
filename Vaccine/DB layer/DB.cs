using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    internal class DB
    {
        private static DB dbInstance;
        private DB() { }

        public static DB DbInstance
        {
            get
            {
                if (dbInstance == null)
                {
                    dbInstance = new DB();
                }
                return dbInstance;
            }
        }
        public static string user = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\vjson.json";
        public static string vaccine = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\vaccines.json";
        public static string vaccinationCenter = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\VaccinationCenter.json";
        public static string patient= @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\Patient.json";
        public  List<User> UserRead()
        {
            var userPath = File.ReadAllText(user);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(userPath);
            return users;
        }
        public  List<Vaccine> VaccineRead()
        {
            var vaccines = File.ReadAllText(vaccine);
            List<Vaccine> allVaccines = JsonConvert.DeserializeObject<List<Vaccine>>(vaccines);
            return allVaccines;
        }

        public List<Patient> PatientRead()
        {
            var patients = File.ReadAllText(patient);
            List<Patient> allpatients = JsonConvert.DeserializeObject<List<Patient>>(patients);
            return allpatients;
        }
        public void AddVaccinationCentertoDB(VaccineCenter vc)
        {
            var vaccinationC = VaccineCenterRead();
            vaccinationC.Add(vc);

            var vcJSON = JsonConvert.SerializeObject(vaccinationC);
            File.WriteAllText(vaccinationCenter, vcJSON);

        }
        public  List<VaccineCenter> VaccineCenterRead()
        {
            var file = File.ReadAllText(vaccinationCenter);
            List<VaccineCenter> v = JsonConvert.DeserializeObject<List<VaccineCenter>>(file);
            return v;
        }
       
        
        public  string GAaddVaccine(string addVaccine,int idoses=0)
        {

            var vc = Vaccine.ViewVaccinesGloballyCenter();
            var vacc = VaccineRead();

            if (vc.Any(v => v.Equals(addVaccine)))
            {
                return "Vaccine is already present";
            }
            var newVaccine = new Vaccine(addVaccine, idoses);
                vacc.Add(newVaccine);

                var vaccineJSON = JsonConvert.SerializeObject(vacc);
                File.WriteAllText(vaccinationCenter, vaccineJSON);
                
               
            
            return "Vaccine added successfully";
        }
        
        public  string addVaccineInCenter(string addVaccine, int doses, string vc,int minAge,int maxAge)
        {
            var vcDetail = DB.dbInstance.VaccineCenterRead();

            foreach (var v in vcDetail)
            {
                if (v.VcName == vc)
                {
                    var newVaccine = new VaccineAvailable(addVaccine, doses,minAge,maxAge);
                    v.vaccines.Add(newVaccine);

                    var vaccineJSON = JsonConvert.SerializeObject(vcDetail);
                    File.WriteAllText(@"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\VaccinationCenter.json", vaccineJSON);

                    return "Vaccine added successfully!";

                }
            }
            return null;
        }
        public  void updateVaccine(int count,string addVaccine, string centerName)
        {
            var paths = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\VaccinationCenter.json";
            List<VaccineCenter> v = DB.DbInstance.VaccineCenterRead();
            int i = 0;
            foreach (var obj in v)
            {
                if (obj.VcName == centerName)
                {
                    var x = obj.vaccines;
                    foreach (var vx in x)
                    {
                        if (vx.VName.Equals( addVaccine )) {
                            vx.vcount = count;
                            
                            var jsonFormattedContent = Newtonsoft.Json.JsonConvert.SerializeObject(v);
                            File.WriteAllText(paths, jsonFormattedContent);
                            return;
                            }
                    }
                }
                i++;
            }

        }
        public void AddUser(User newUser)
        {
            var users = UserRead();
            users.Add(newUser);
            
            var userJSON = JsonConvert.SerializeObject(users);
            File.WriteAllText(user, userJSON);
            Console.WriteLine("User added succesfully!");
           

        }





    }
}

