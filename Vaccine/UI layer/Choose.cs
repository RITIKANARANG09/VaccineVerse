
namespace Project
{
    internal class Choose
    {
        public static void MainFunctionChoose()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Choose a number:");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Signup (For Patients) ");
            Console.WriteLine("3. View Vaccines");
            Console.WriteLine("4. Exit");
        }
        public static void GlobalAdminUIChoose()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Choose any one : ");
            Console.WriteLine("1. Add admin");
            Console.WriteLine("2. Add vaccine");
            Console.WriteLine("3. View vaccines");
            Console.WriteLine("4. View Admins");
            Console.WriteLine("5. Exit");
        }
        public static void LocalAdminUIChoose()
        {
         Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Choose anyone number : ");
            Console.WriteLine("1 View available Vaccines");
            Console.WriteLine("2 Increment Vaccines ");
            Console.WriteLine("3 Decrement Vaccines ");
            Console.WriteLine("4 Record of patients ");
            Console.WriteLine("5 Add vaccine to center ");
        }
        public static void PatientUIChoose()
        {
            Console.WriteLine("Choose any one : ");
            Console.WriteLine("1. View Vaccines");
            Console.WriteLine("2. View Past Records");
            //Console.WriteLine("3. Get Certificates");
            Console.WriteLine("4. Exit");
        }
        public static void HelperChoose()
        {
            Console.WriteLine("Press 1 to go back");
            Console.WriteLine("Press 2 to exit");
        }
        public static void VaccineSelectChoose()
        {
            Console.WriteLine("1. Select vaccine :");
            Console.WriteLine("2. Exit");
        }
    }
}
