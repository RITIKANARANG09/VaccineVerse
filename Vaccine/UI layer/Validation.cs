
using System.Globalization;

namespace Project
{
    public class Validation
    {
        public static int IntValidate()
        {
        IntVOption: Console.WriteLine("Choose any number: ");
            int input;
            try
            {
                input = Convert.ToInt32(Console.ReadLine());
                return input;
            }
            catch
            {
                ExceptionController.OnlyNumeric();
                goto IntVOption;
            }
        }
        public static DateTime DateValidate()
        {
           
        enterDate: string input;
            try
            {

                input = Console.ReadLine();
                DateTime date = DateTime.ParseExact(input, "yyyy-MM-ddTHH:mm:ss", CultureInfo.CurrentCulture);
                return date;
            }
            catch
            {
                Console.WriteLine("Enter the date in correct format!");
                goto enterDate;
            };
        }
    }
}
