using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
/*using Vaccine;*/

namespace Project
{
    public class VaccineAvailable
    {
        public  string VName;
        public  int vcount;
        public int minAge;
        public int maxAge;
       /* public VaccineAvailable(string name,int count)
        {
            this.VName = name;
                this.vcount = count;
        }*/
        public VaccineAvailable(string name, int count,int minage=0,int maxage=100)
        {
            this.VName = name;
            this.vcount = count;
            this.minAge = minage;
            this.maxAge = maxage;
        }
    }
    public class VaccineCenter
    {
        public string VcName { get; set; }
        public string LaName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public List<VaccineAvailable> vaccines;
        public List<Appointment> appointmentDate;

        public static string incrementVaccines(int ivcount, string vaccineName, string vc)
        {
            var vaccineCenter = DB.DbInstance.VaccineCenterRead();
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
        public static void AddNewVaccinationCenter(User user, string vcName)
        {
            VaccineCenter vc = new VaccineCenter
            {
                VcName = vcName,
                LaName = user.Username,
                username = user.Username,
                password = user.Password,
                role = Role.Admin.ToString(),
                vaccines = new List<VaccineAvailable>(),
                appointmentDate=new List<Appointment>()
            };

            DB.DbInstance.AddVaccinationCentertoDB(vc);

        }
        public static string decrementVaccines(int ivcount, string vaccineName, string vc)
        {
            var vaccineCenter = DB.DbInstance.VaccineCenterRead();
            foreach (var v in vaccineCenter)
            {
                if (v.VcName == vc)
                {
                    var r = v.vaccines;

                    bool flag = false;
                    foreach (var r2 in r)
                    {
                        if (r2.VName == vaccineName && r2.vcount>0 && r2.vcount>=ivcount)
                        {
                            //int x = r2.vcount + ivcount;
                            Console.WriteLine("ivcount is : "+ivcount);
                             r2.vcount=r2.vcount-ivcount;
                            flag = true;
                            DB.DbInstance.updateVaccine(r2.vcount, vaccineName, v.VcName);
                            ViewVaccinesLocally(vc);
                            return "Vaccine decremented successfully";
                        }
                        else if (r2.VName == vaccineName &&  r2.vcount == 0)
                        {
                            return "Currently, this vaccine is out of stock.";
                        }
                        else if(r2.VName == vaccineName &&  r2.vcount<ivcount)
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

            var vc = DB.DbInstance.VaccineCenterRead();
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
        public static List<Appointment> RecordOfPatientsOfVC(string VcName)
        {
            var readVaccineCenter = DB.DbInstance.VaccineCenterRead();
            List<Appointment> patientDetails= new List<Appointment>();
            foreach(var vaccineCenter in readVaccineCenter)
            {
                if(vaccineCenter.VcName == VcName)
                {
                    foreach(var appointments in vaccineCenter.appointmentDate)
                    patientDetails.Add(appointments);
                }
            }
            return patientDetails;
        }
        public static List<string> ViewVaccineByAge(int age)
        {
            var readvaccine = DB.DbInstance.VaccineCenterRead();
            List<string> list=new List<string>();
            foreach(var v in readvaccine)
            {
                var s = v.vaccines;
                foreach(var s2 in s)
                {
                    if (s2.minAge <= age && s2.maxAge >= age && !list.Contains(s2.VName))
                        list.Add(s2.VName);
                }
            }
            return list;
        }
        public static List<VaccineCenter> GiveCertificateToPatient(string pn,string vname)
        {
            var vaccineCenterRead = DB.DbInstance.VaccineCenterRead();
            var vaccineCenter=vaccineCenterRead.FindAll(v => v.vaccines.Equals(pn) && v.vaccines.Equals(vname));
            return vaccineCenter;
        }   
        
    }
}