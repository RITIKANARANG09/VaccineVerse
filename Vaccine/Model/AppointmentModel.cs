
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
    }
        
}
