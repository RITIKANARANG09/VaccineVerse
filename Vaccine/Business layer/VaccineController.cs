using Newtonsoft.Json;
namespace Project
{
    public class VaccineController
    {
        public static List<string> ViewVaccinesGloballyCenter()
        {
            int i = 1;
            List<string> list = new List<string>();
            var vaccineCenter = DB.DbInstance.VaccineCenterRead;
            foreach (VaccineCenter v in vaccineCenter)
            {
                var r = v.vaccines;
                foreach (var r2 in r)
                {
                    if (r2.vcount > 0 && !list.Contains(r2.VName))
                        list.Add(r2.VName);
                }
            }
            var vaccines = DB.DbInstance.VaccineRead;
            foreach(var v in vaccines)
            {
                if(!list.Contains(v.vname.ToString()))
                list.Add(v.vname.ToString());
            }
           
            return list;
        }
        public static List<string> FindParticularVaccineCenterWise(string vaccine)
        {
            List<string> list = new List<string>();
            var vaccineCenter = DB.DbInstance.VaccineCenterRead;
            foreach (VaccineCenter v in vaccineCenter)
            {
                var r = v.vaccines;
                foreach (var r2 in r)
                {
                    if (r2.vcount > 0 && r2.VName == vaccine)
                        list.Add(v.VcName);
                }
            }
            return list;
        }
        public static string AddVaccineGlbally(string addVaccine,int idoses)
        {
            var input = ViewVaccinesGloballyCenter();
            if(input.Any(v=>v.Equals(addVaccine)))
                return "Vaccine already present !";
            Vaccine v = new Vaccine(addVaccine,idoses);
            var p = DB.DbInstance.VaccineRead;
            p.Add(v);
            try
            {
                var vaccineJSON = JsonConvert.SerializeObject(p);
                File.WriteAllText(@"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\vaccines.json", vaccineJSON);
                return "Vaccine added successfully!";
            }
            catch
            {
                ExceptionController.DbException();
                return null;
            }
        }
        public static string incrementVaccines(int ivcount, string vaccineName, string vc)
        {
            var vaccineCenter = DB.DbInstance.VaccineCenterRead;
            foreach (var v in vaccineCenter)
            {
                if (v.VcName == vc)
                {
                    var r = v.vaccines;

                    bool flag = false;
                    foreach (var r2 in r)
                    {
                        if (r2.VName == vaccineName)
                        {
                            //int x = r2.vcount + ivcount;
                            r2.vcount += ivcount;
                            flag = true;
                            DB.DbInstance.updateVaccine(r2.vcount, vaccineName, v.VcName);
                            ViewVaccinesLocally(vc);
                            return "Vaccine incremented successfully";
                        }
                    }
                }

            }
            return "This vaccine is not available currently in the center ";
        }
        public static string decrementVaccines(int ivcount, string vaccineName, string vc)
        {
            var vaccineCenter = DB.DbInstance.VaccineCenterRead;
            foreach (var v in vaccineCenter)
            {
                if (v.VcName == vc)
                {
                    var r = v.vaccines;

                    bool flag = false;
                    foreach (var r2 in r)
                    {
                        if (r2.VName == vaccineName && r2.vcount > 0 && r2.vcount >= ivcount)
                        {
                            //int x = r2.vcount + ivcount;
                            Console.WriteLine("ivcount is : " + ivcount);
                            r2.vcount = r2.vcount - ivcount;
                            flag = true;
                            DB.DbInstance.updateVaccine(r2.vcount, vaccineName, v.VcName);
                            ViewVaccinesLocally(vc);
                            return "Vaccine decremented successfully";
                        }
                        else if (r2.VName == vaccineName && r2.vcount == 0)
                        {
                            return "Currently, this vaccine is out of stock.";
                        }
                        else if (r2.VName == vaccineName && r2.vcount < ivcount)
                        {
                            return $"Only {r2.vcount} vaccines are currently available.";
                        }
                    }
                }

            }
            return "This vaccine is not available currently in the center ";
        }
        public static List<VaccineAvailable> ViewVaccinesLocally(string vaccineCenters)
        {

            var vc = DB.DbInstance.VaccineCenterRead;
            List<VaccineAvailable> va = new List<VaccineAvailable>();
            foreach (var v in vc)
            {
                if (v.VcName == vaccineCenters)
                {
                    var r = v.vaccines;

                    foreach (var r2 in r)
                    {
                        if (r2 != null)
                        { va.Add(r2); }
                    }
                }
            }

            return va;
        }
        public static List<string> ViewVaccineByAge(int age)
        {
            var readvaccine = DB.DbInstance.VaccineCenterRead;
            List<string> list = new List<string>();
            foreach (var v in readvaccine)
            {
                var s = v.vaccines;
                foreach (var s2 in s)
                {
                    if (s2.minAge <= age && s2.maxAge >= age && !list.Contains(s2.VName))
                        list.Add(s2.VName);
                }
            }
            return list;
        }
        public static string VaccineAddInCenter(string vc, string vaccineName, int idoses, int minAge, int maxAge)

        {

            var readVaccinesInVaccinationCenter = DB.DbInstance.VaccineCenterRead;

            var vaccineCenter = readVaccinesInVaccinationCenter.Find(v => v.VcName == vc);
            var vaccineFind = vaccineCenter.vaccines.Find(vacc => vacc.VName == vaccineName);
            if (vaccineFind == null)
            {
                string input = DB.DbInstance.addVaccineInCenter(vaccineName, idoses, vc, minAge, maxAge);
                if (input != null)
                {
                    return input;
                }
                else
                {
                    return "Vaccine can't be added";
                }
            }
            return null;
        }

    }
}
