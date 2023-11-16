namespace Project
{
    public interface IVaccineControllerForAdmin
    {
        bool VaccineAddInCenter(VaccineCenter vaccineCenter);
        bool UpdateVaccine(Vaccine vaccineObj, string updateCheck, int vaccineCount);
       
        List<Vaccine> ViewVaccinesCenterWise(VaccineCenter vaccineCenterObject);


    }
}