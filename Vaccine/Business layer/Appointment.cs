using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Appointment
    {
        public string patientPhoneNo;
        public string VName;
 
        public DateTime dt;
        public Appointment(string phoneNo, string VName, DateTime date)
        {
            this.patientPhoneNo = phoneNo;
            this.dt = date;
            this.VName = VName;
        }
        public static bool bookApt(string phoneNo,string VcName,DateTime dt,string VName)
        {
            Appointment appointment=new Appointment(phoneNo,VName,dt);
            var readVaccineCenter = DB.DbInstance.VaccineCenterRead();
           foreach(var vc in readVaccineCenter)
            {
                if(vc.VcName==VcName)
                {
                    vc.appointmentDate.Add(appointment);
                    break;
                }
            }
            //var vc= readVaccineCenter.Find(v => v.appointmentDate[0] =phoneNo);
            
            var appointments= JsonConvert.SerializeObject(readVaccineCenter);
            File.WriteAllText(@"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\VaccinationCenter.json", appointments);
            return true;
        }
        
    }
}
