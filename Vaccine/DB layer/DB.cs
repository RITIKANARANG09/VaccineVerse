
namespace Project
{
    internal class DB
    {
        private static DB dbInstance;

        
        
       
        
        
        
        
        
        
        
        
        private DB() {

           
           
           
           
           
            try
            {


               

               

                


                


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
        /*public void AddVaccinationCentertoDB(VaccineCenter vc)
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
            

        }*/
        
/*        public  string GAaddVaccine(string addVaccine,int idoses=0)
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
*/
       /* public string addVaccineInCenter(string addVaccine, int doses, string vc, int minAge, int maxAge)
        {
            var vcDetail = VaccineCenterRead;

            foreach (var v in vcDetail)
            {
                if (v.VcName == vc)
                {
                    var newVaccine = new VaccineAvailable(addVaccine, doses, minAge, maxAge);
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
        }*/
        /*public  void updateVaccine(int count,string addVaccine, VaccineCenter vaccineCenterObj)
        {
            var paths = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\VaccinationCenter.json";
            List<VaccineCenter> v = VaccineCenterRead;
            int i = 0;
            foreach (var obj in v)
            {
                if (obj.VcName == vaccineCenterObj.VcName)
                {
                    var vaccineList= obj.vaccines;
                    foreach (var vaccine in vaccineList)
                    {
                        if (vaccine.VName.Equals( addVaccine )) {
                            vaccine.vcount = count;
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

        }*/
      /*  public void AddUser(User newUser)
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

        }*/

    }
}

