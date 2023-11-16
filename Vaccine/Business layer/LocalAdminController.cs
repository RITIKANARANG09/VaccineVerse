

namespace Project
{
    public class LocalAdminController
    {
        public List<VaccineCenter> ViewAdmins()
        {
            return VaccineCenterDataBase.VaccineCenterInstance.VaccineCenterList;
        }
    }
}
