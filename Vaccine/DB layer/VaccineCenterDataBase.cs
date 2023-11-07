
using Newtonsoft.Json;

namespace Project
{
    public class VaccineCenterDataBase : DataBase<VaccineCenter>
    {
        private static VaccineCenterDataBase _vaccineCenterInstance;
        public List<VaccineCenter> VaccineCenterList;
        private static string _vaccinationCenterPath;
        private VaccineCenterDataBase()
        {
            VaccineCenterList = new List<VaccineCenter>();
            _vaccinationCenterPath = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\VaccinationCenter.json";
            try
            {
                var file = File.ReadAllText(_vaccinationCenterPath);
                VaccineCenterList = JsonConvert.DeserializeObject<List<VaccineCenter>>(file);
            }
            catch
            {
                ExceptionController.DbException();
            }
        }
        public static VaccineCenterDataBase VaccineCenterInstance
        {
            get
            {
                if (_vaccineCenterInstance == null)
                {
                    _vaccineCenterInstance = new VaccineCenterDataBase();
                }
                return _vaccineCenterInstance;
            }
        }
        public bool addVaccineInCenter(List<VaccineCenter> CenterList)
        {
 

                    
                    UpdateItem(_vaccinationCenterPath, CenterList);
                    return true;

                
            
            return false;
        }
        public void AddVaccinationCentertoDB(VaccineCenter vaccineCenterObject)
        {
            AddItem(vaccineCenterObject, VaccineCenterList, _vaccinationCenterPath);
        }
        public bool updateVaccine(List<VaccineCenter> CenterList)
        {
        return UpdateItem(_vaccinationCenterPath, CenterList);
         }
               
    }
    }
