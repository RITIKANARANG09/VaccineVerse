
namespace Project
{
    public class ExceptionController
    {
        public static void DbException()
        {
            Console.WriteLine("An unexpected error occurred!!");
        }
        public static void OnlyNumeric()
        {
            Console.WriteLine("Only numerical values are allowed");
        }
        public static void SomethingWentWrong()
        {
            Console.WriteLine("Something went wrong");
        }
        public static void NotValid()
        {
            Console.WriteLine("Enter a valid input !");
        }
        public static void Message(string message)
        {
            Console.WriteLine(message);
        }

    }
}
