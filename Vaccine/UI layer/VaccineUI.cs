
namespace Project
{
    public class VaccineUI
    {
        public static void ViewVaccine()
        {
            List<string> lists = VaccineController.ViewVaccinesGloballyCenter();
            Console.WriteLine("Vaccine available globally are : ");
            string result = string.Join(", ", lists);
            Console.WriteLine(result);
        }

        public static void AddVaccineByGA()
        {
            while (true)
            {

                Console.WriteLine("Which vaccine you want to add");
                var addVaccine = Console.ReadLine();
            AddVaccineByGAChoose:
                Console.WriteLine("how much doses you want to add initially ? ");
                int idoses;
                try
                {
                    idoses = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    ExceptionController.OnlyNumeric();
                    goto AddVaccineByGAChoose;
                }
                var vaccineG = VaccineController.ViewVaccinesGloballyCenter();
                if (vaccineG.Any(v => v.Equals(vaccineG)))
                {
                    Console.WriteLine("Vaccine already present ");
                    continue;
                }
                var input = VaccineController.AddVaccineGlbally(addVaccine, idoses);
                Console.WriteLine(input);


                break;
            }
        }
        public static void IncreaseVaccineCount(string vc)
        {

            Console.WriteLine(Message.inputIncrementVaccineName);
            List<VaccineAvailable> VaccineNameList = VaccineController.ViewVaccinesLocally(vc);
            string result = string.Join(", ", VaccineNameList.Select(v => v.VName));
            Console.WriteLine(result);
            string vaccineName = Console.ReadLine();
        incDosesOptions: Console.Write($"How many doses of {vaccineName} do you want to increase: ");
            int ivcount;
            try
            {
                ivcount = Convert.ToInt32(Console.ReadLine());
                string input = VaccineController.incrementVaccines(ivcount, vaccineName, vc);
                Console.WriteLine(input);
            }
            catch
            {
                ExceptionController.OnlyNumeric();
                goto incDosesOptions;
            }
        }
    }
}
