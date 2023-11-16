namespace Project
{
    public interface IVaccineCenterDataBase
    {
        bool AddVaccinationCentertoDB(VaccineCenter vaccineCenterObject);
        bool addVaccineInCenter(List<VaccineCenter> CenterList);
        bool updateVaccine(List<VaccineCenter> CenterList);
    }
}