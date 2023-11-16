namespace Project
{
    public class DataBase<T> : IDataBase<T> where T : class
    {
        protected DataBase()
        {

        }
        public bool AddItem(T obj, List<T> list, string path)
        {
            list.Add(obj);
            if (UpdateItem(path, list))
                return true;
            return false;
        }
        public bool UpdateItem(string path, List<T> list)
        {
            try
            {
                var jsonFormattedContent = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                File.WriteAllText(path, jsonFormattedContent);
            }
            catch(Exception ex)
            {
                ExceptionController.LogException(ex, "Error occured while updating DB");
                return false;
            }
            return true;
        }

    }
}
