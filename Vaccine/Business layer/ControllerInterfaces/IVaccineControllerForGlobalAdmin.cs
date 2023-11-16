namespace Project
{
    public interface IVaccineControllerForGlobalAdmin
    {
        List<Vaccine> ViewVaccinesGlobally();
        bool AddVaccineGlobally(Vaccine vaccine);
    }
}