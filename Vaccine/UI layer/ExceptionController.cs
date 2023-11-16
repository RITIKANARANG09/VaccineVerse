
namespace Project
{
    public class ExceptionController
    {
       
        public static void LogException(Exception ex,string error)
        {
            string path = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\ExceptionHandler.txt";
            var time = DateTime.Now.ToString();
            string text = "EXCEPTION::   " + ex.ToString() + "\n\nERROR::   " + error + "\n" + time + "\n\n";
            File.AppendAllText(path, text);
        }
    }
}
