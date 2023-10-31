
namespace Project
{
    
    public class VaccineCenter
    {
        public string VcName { get; set; }
        public string LaName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public List<VaccineAvailable> vaccines;
        public List<Appointment> appointmentDate;

    }
}
