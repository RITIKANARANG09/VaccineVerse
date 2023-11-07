
using Newtonsoft.Json;

namespace Project
{
    public class VaccineDataBase:DataBase<Vaccine>
    {
        private static VaccineDataBase _vaccineInstance;
        private static string _vaccinePath;
        public List<Vaccine> vaccineList;
        public static VaccineDataBase VaccineInstance
        {
            get
            {
                if (_vaccineInstance == null)
                {
                    _vaccineInstance = new VaccineDataBase();
                }
                return _vaccineInstance;
            }
        }
        private VaccineDataBase() {
            _vaccinePath = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\vaccines.json";
            vaccineList = new List<Vaccine>();
            try {
                var vaccines = File.ReadAllText(_vaccinePath);
                vaccineList = JsonConvert.DeserializeObject<List<Vaccine>>(vaccines);
            }
            catch
            {

            }
            }
        public bool GloballyAddVaccine(Vaccine newVaccine)
        {

            return AddItem(newVaccine, vaccineList, _vaccinePath);
          
        }


    }
}
