using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    internal class Vaccine
    {

        //public int vid;
        public string vname;
        //public string description;
        public int count;
        public Vaccine(string name, int count = 0)
        {
            this.vname = name;
            this.count = count;
        }
        public Vaccine() { count = 0; }
        public static List<string> ViewVaccinesGloballyCenter()
        {
            int i = 1;
            List<string> list = new List<string>();
            var vaccineCenter = DB.DbInstance.VaccineCenterRead();
            foreach (VaccineCenter v in vaccineCenter)
            {
                var r = v.vaccines;
                foreach (var r2 in r)
                {
                    if (r2.vcount > 0 && !list.Contains(r2.VName))
                        list.Add(r2.VName);
                }
            }
            var vaccines = DB.DbInstance.VaccineRead();
            foreach(var v in vaccines)
            {
                if(!list.Contains(v.vname.ToString()))
                list.Add(v.vname.ToString());
            }
           
            return list;
        }
        public static List<string> FindVaccineCenterWise(string vaccine)
        {
            List<string> list = new List<string>();
            var vaccineCenter = DB.DbInstance.VaccineCenterRead();
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
            var p = DB.DbInstance.VaccineRead();
            p.Add(v);
            var vaccineJSON = JsonConvert.SerializeObject(p);
            File.WriteAllText(@"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\vaccines.json", vaccineJSON);
            return "Vaccine added successfully!";
        }
    }
}
