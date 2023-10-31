
using Vaccine.Model;

namespace Project
{
    public class Patient:User
    {
        public int PatientId { get; set; }
        public List<string> VName { get; set; }
        public string phoneNo { get; set; }
        List<Appointment> appointments { get; set; }
    }
}
