
namespace Project
{
    public class Vaccine
    {
        public string vname;
        public int count;
        public Vaccine(string name, int count = 0)
        {
            this.vname = name;
            this.count = count;
        }
        public Vaccine() { count = 0; }
    }
}
