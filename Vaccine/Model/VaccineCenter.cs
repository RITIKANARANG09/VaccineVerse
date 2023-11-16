
namespace Project
{
    
    public class VaccineCenter
    {
        public string VcName { get; set; }
        public string LaName { get; set; }
        public string username { get; set; }
        public int vcount { get; set; }
        public List<Vaccine> vaccines;
        //public Dictionary<Vaccine, int> vaccines;
        public string Address { get; set; }
        public int VcId { get; set; }
        public static int vCenterIDInc = VaccineCenterDataBase.VaccineCenterInstance.VaccineCenterList[VaccineCenterDataBase.VaccineCenterInstance.VaccineCenterList.Count - 1].VcId;
    }
}
