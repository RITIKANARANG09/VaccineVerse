
using System.Globalization;

namespace Project
{
    public class Validation
    {
        public static int IntValidate()
        {
            while (true)
            {
                Console.WriteLine("Choose any number: ");
                int input;
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    return input;
                }
                catch(Exception ex) 
                {
                    Errors.OnlyNumeric();
                    ExceptionController.LogException(ex, "Not an integer value");
                }
            }
        }
        public static DateTime DateValidate()
        {
            while (true)
            {
                string input;
                try
                {

                    input = Console.ReadLine();
                    DateTime date = DateTime.ParseExact(input, "yyyy-MM-ddTHH:mm:ss", CultureInfo.CurrentCulture);
                    return date;
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Enter the date in correct format!");
                    ExceptionController.LogException(ex, "Wrong date format !!");
                    continue;
                };
            }
        }
    }
}
