
namespace Project
{
    public class Vaccine
    {
        public string vname;
        public int vcount;
        public int minAge;
        public int maxAge;
        
        public Vaccine(string name, int minAge,int maxAge,int vcount=10000)
        {
            this.vname = name;
            //this.vid = i++;
            this.vcount = vcount;
            this.minAge = minAge;
            this.maxAge = maxAge;
        }
        
    }
}
