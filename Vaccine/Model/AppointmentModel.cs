
namespace Project
{

    public class Appointment
    {
        public string PatientPhoneNo;
        public string VName;
        public DateTime Date;
        public string VcName;
        public string Address;
        public Appointment(string phoneNo, string VName, DateTime date,string VcName, string address)
        {
            this.PatientPhoneNo = phoneNo;
            this.Date = date;
            this.VName = VName;
            this.VcName = VcName;
            Address = address;
        }
    }
        
}
