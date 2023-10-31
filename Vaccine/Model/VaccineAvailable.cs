
namespace Project
{
    public class VaccineAvailable
    {
        public string VName;
        public int vcount;
        public int minAge;
        public int maxAge;
        public VaccineAvailable(string name, int count, int minage = 0, int maxage = 100)
        {
            this.VName = name;
            this.vcount = count;
            this.minAge = minage;
            this.maxAge = maxage;
        }
    }
}
