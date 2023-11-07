
namespace Project
{

    public class Appointment
    {
        public string patientPhoneNo;
        public string VName;

        public DateTime dt;
        public string VcName;
        public Appointment(string phoneNo, string VName, DateTime date,string VcName)
        {
            this.patientPhoneNo = phoneNo;
            this.dt = date;
            this.VName = VName;
            this.VcName = VcName;
        }
    }
        
}
