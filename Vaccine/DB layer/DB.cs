using Newtonsoft.Json;

using Vaccine.Model;

namespace Project
{
    internal class DB
    {
        private static DB dbInstance;

        public static string user = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\vjson.json";
        public static string vaccine = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\vaccines.json";
        public static string vaccinationCenter = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\VaccinationCenter.json";
        public static string patient = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\Patient.json";

        public List<User> usersRead;
        public List<VaccineCenter> VaccineCenterRead;
        public List<Vaccine> VaccineRead;
        public List<Patient> PatientRead;
       private DB() {

            usersRead = new List<User>();
            VaccineCenterRead = new List<VaccineCenter>();
            VaccineRead = new List<Vaccine>();
            PatientRead = new List<Patient>();

            try
            {


                var allUsers = File.ReadAllText(user);
                if (!String.IsNullOrEmpty(allUsers))
                usersRead = JsonConvert.DeserializeObject<List<User>>(allUsers);

                var file = File.ReadAllText(vaccinationCenter);
                VaccineCenterRead = JsonConvert.DeserializeObject<List<VaccineCenter>>(file);

                var vaccines = File.ReadAllText(vaccine);
                VaccineRead = JsonConvert.DeserializeObject<List<Vaccine>>(vaccines);

                var patients = File.ReadAllText(patient);
                PatientRead = JsonConvert.DeserializeObject<List<Patient>>(patients);


            }
            catch
            {
                ExceptionController.DbException();
            }
        }

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
        public void AddVaccinationCentertoDB(VaccineCenter vc)
        {
            var vaccinationC = VaccineCenterRead;
            vaccinationC.Add(vc);
            try
            {
                var vcJSON = JsonConvert.SerializeObject(vaccinationC);
                File.WriteAllText(vaccinationCenter, vcJSON);
            }
            catch
            {
                ExceptionController.DbException();
            }
            

        }
        
        public  string GAaddVaccine(string addVaccine,int idoses=0)
        {

            var vc = VaccineController.ViewVaccinesGloballyCenter();
            var vacc = VaccineRead;

            if (vc.Any(v => v.Equals(addVaccine)))
            {
                return "Vaccine is already present";
            }
            var newVaccine = new Vaccine(addVaccine, idoses);
                vacc.Add(newVaccine);
            try
            {
                var vaccineJSON = JsonConvert.SerializeObject(vacc);
                File.WriteAllText(vaccinationCenter, vaccineJSON);
            }
            catch { ExceptionController.DbException(); }
               
            
            return "Vaccine added successfully";
        }
        
        public  string addVaccineInCenter(string addVaccine, int doses, string vc,int minAge,int maxAge)
        {
            var vcDetail = VaccineCenterRead;

            foreach (var v in vcDetail)
            {
                if (v.VcName == vc)
                {
                    var newVaccine = new VaccineAvailable(addVaccine, doses,minAge,maxAge);
                    v.vaccines.Add(newVaccine);
                    try
                    {
                        var vaccineJSON = JsonConvert.SerializeObject(vcDetail);
                        File.WriteAllText(@"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\VaccinationCenter.json", vaccineJSON);
                    }
                    catch { ExceptionController.DbException(); }
                    return "Vaccine added successfully!";

                }
            }
            return null;
        }
        public  void updateVaccine(int count,string addVaccine, string centerName)
        {
            var paths = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\VaccinationCenter.json";
            List<VaccineCenter> v = VaccineCenterRead;
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
                            try
                            {
                                var jsonFormattedContent = Newtonsoft.Json.JsonConvert.SerializeObject(v);
                                File.WriteAllText(paths, jsonFormattedContent);

                                return;
                            }
                            catch { ExceptionController.DbException(); }
                            }
                    }
                }
                i++;
            }

        }
        public void AddUser(User newUser)
        {
            var users = usersRead;
            users.Add(newUser);
            try
            {
                var userJSON = JsonConvert.SerializeObject(users);
                File.WriteAllText(user, userJSON);
                Console.WriteLine("User added succesfully!");
            }
            catch
            {
                ExceptionController.DbException();
            }

        }

    }
}

