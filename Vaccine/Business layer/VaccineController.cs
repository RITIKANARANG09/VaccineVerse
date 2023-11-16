
using Project;

namespace Project
{

    public class VaccineController : IVaccineControllerForAdmin, IVaccineControllerForGlobalAdmin, IVaccineControllerForPatient
    {
        //public List<VaccineCenter> VaccineCentersList = ConnectToDataBase.DataBaseInstance.readVaccineCenterList();
        //public List<Vaccine> VaccinesList = ConnectToDataBase.DataBaseInstance.readVaccineList();
        //IVaccineDataBase vaccineDataBase = VaccineDataBase.VaccineInstance;
        public List<Vaccine> ViewVaccinesGlobally()
        {

            var vaccineList = new List<Vaccine>();
            foreach (var vaccine in VaccineDataBase.VaccineInstance.vaccineList)
            {
                if (!vaccineList.Contains(vaccine))
                    vaccineList.Add(vaccine);
            }

            return vaccineList;
        }

        public bool AddVaccineGlobally(Vaccine vaccine)
        {


            return VaccineDataBase.VaccineInstance.GloballyAddVaccine(vaccine);

        }
        /* public bool IncreaseVaccineCount(Vaccine vaccineObj,VaccineCenter vaccineCenter, int doses)
         {
             Dictionary<Vaccine, int> v = vaccineCenter.vaccines;
             if(v.ContainsKey(vaccineObj))
             {
                 v[vaccineObj] += doses;
             }
             return VaccineCenterDataBase.VaccineCenterInstance.updateVaccine(VaccineCenterDataBase.VaccineCenterInstance.VaccineCenterList);
         }
         public bool DecreaseVaccineCount(Vaccine vaccineObj, VaccineCenter vaccineCenter, int doses)
         {
             Dictionary<Vaccine, int> v = vaccineCenter.vaccines;
             if (v.ContainsKey(vaccineObj))
             {
                 v[vaccineObj] -= doses;
             }
             return VaccineCenterDataBase.VaccineCenterInstance.updateVaccine(VaccineCenterDataBase.VaccineCenterInstance.VaccineCenterList);*/
        //}
        public bool UpdateVaccine(Vaccine vaccineObj, string updateCheck, int vaccineCount)
        {
           /* var vaccineObj=vaccineCenterObj.vaccines.Find(vaccine=>vaccine.VName.Equals(vaccineName));
            if (vaccineObj == null) */
            if (updateCheck.Equals("increase"))
                vaccineObj.VCount += vaccineCount;
            else if (updateCheck.Equals("decrease"))
                vaccineObj.VCount -= vaccineCount;
            return VaccineCenterDataBase.VaccineCenterInstance.updateVaccine(VaccineCenterDataBase.VaccineCenterInstance.VaccineCenterList);
        }

        public List<Vaccine> ViewVaccinesCenterWise(VaccineCenter vaccineCenterObject)
        {
            var vaccines = new List<Vaccine>();
            foreach (var v in vaccineCenterObject.vaccines)
                vaccines.Add(v);
            return vaccines;
        }
        public List<VaccineCenter> ViewCenterByVaccine(string vaccine)
        {
            var centerList = new List<VaccineCenter>();

            foreach (VaccineCenter vaccineCenter in VaccineCenterDataBase.VaccineCenterInstance.VaccineCenterList)
            {
                var vaccinesList = vaccineCenter.vaccines;
                foreach (var vaccines in vaccinesList)
                {
                    if (vaccines.VCount > 0 && vaccines.VName.Equals(vaccine))
                        centerList.Add(vaccineCenter);
                }
            }
            return centerList;
        }
        public List<Vaccine> ViewVaccineByAge(int age)
        {

            var vaccineList = new List<Vaccine>();
            foreach (var vaccine in VaccineDataBase.VaccineInstance.vaccineList)
            {

                if (vaccine.MinAge <= age && vaccine.MaxAge >= age && !vaccineList.Contains(vaccine))
                    vaccineList.Add(vaccine);
            }
            return vaccineList;
        }

        public bool VaccineAddInCenter(VaccineCenter vaccineCenter)

        {

            var input = VaccineCenterDataBase.VaccineCenterInstance.addVaccineInCenter(VaccineCenterDataBase.VaccineCenterInstance.VaccineCenterList);
            return input;
        }

    }
}