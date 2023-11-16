namespace Project
{
    public interface IVaccineControllerForPatient
    {
        List<Vaccine> ViewVaccineByAge(int age);
        List<VaccineCenter> ViewCenterByVaccine(string vaccine);
    }
}