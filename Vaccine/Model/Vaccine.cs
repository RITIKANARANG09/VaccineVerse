
namespace Project
{
    public class Vaccine
    {
        public string VName;
        public int VCount;
        public int MinAge;
        public int MaxAge;
        public int Id;
        public static int idInc = VaccineDataBase.VaccineInstance.vaccineList[VaccineDataBase.VaccineInstance.vaccineList.Count - 1].Id;
        public Vaccine(string name, int minAge,int maxAge,int vcount=10000)
        {
            this.VName = name;
            this.Id = ++idInc;
            this.VCount = vcount;
            this.MinAge = minAge;
            this.MaxAge = maxAge;
        }
        
    }
}
