
namespace Project
{
    public class VaccineController
    {
        public List<VaccineCenter> VaccineCentersList = ConnectToDataBase.DataBaseInstance.readVaccineCenterList();
        public List<Vaccine> VaccinesList = ConnectToDataBase.DataBaseInstance.readVaccineList();
        public List<Vaccine> ViewVaccinesGlobally()
        {
            
            List<Vaccine> vaccineList= new List<Vaccine>();
            foreach(var vaccine in VaccinesList)
            {
                if(!vaccineList.Contains(vaccine))
                    vaccineList.Add(vaccine);
            }
           
            return vaccineList;
        }
        public  List<string> FindParticularVaccineCenterWise(string vaccine)
        {
            List<string> list = new List<string>();
           
            foreach (VaccineCenter vaccineCenter in VaccineCentersList)
            {
                var vaccinesList = vaccineCenter.vaccines;
                foreach (var vaccines in vaccinesList)
                {
                    if (vaccines.vcount > 0 && vaccines.vname == vaccine)
                        list.Add(vaccineCenter.VcName);
                }
            }
            return list;
        }
        public bool AddVaccineGlobally(Vaccine vaccine)
        {
           
            
            return VaccineDataBase.VaccineInstance.GloballyAddVaccine(vaccine);
            
        }
        public bool UpdateVaccine(Vaccine vaccineObj,string updateCheck,int vaccineCount)
        {
            if (updateCheck == "increase")
                vaccineObj.vcount += vaccineCount;
            else if(updateCheck=="decrease")
                vaccineObj.vcount -= vaccineCount;
            return VaccineCenterDataBase.VaccineCenterInstance.updateVaccine(VaccineCentersList);
        }
       
        public  List<Vaccine> ViewVaccinesLocally(VaccineCenter vaccineCenterObject)
        {
                    return vaccineCenterObject.vaccines;
        }
        public  List<string> ViewVaccineByAge(int age)
        {
           
            List<string> list = new List<string>();
            foreach (var vaccineCenter in VaccineCentersList)
            {
                var vaccinesList = vaccineCenter.vaccines;
                foreach (var vaccine in vaccinesList)
                {
                    if (vaccine.minAge <= age && vaccine.maxAge >= age && !list.Contains(vaccine.vname))
                        list.Add(vaccine.vname);
                }
            }
            return list;
        }
        public bool VaccineAddInCenter(VaccineCenter vaccineCenter, Vaccine vaccineObj,int doses)

        {
            vaccineObj.vcount = doses;
            vaccineCenter.vaccines.Add(vaccineObj);
            var input = VaccineCenterDataBase.VaccineCenterInstance.addVaccineInCenter(VaccineCentersList);
            return input;
        }

    }
}
