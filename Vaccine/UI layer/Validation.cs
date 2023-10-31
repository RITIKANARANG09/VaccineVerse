
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
    }
}
