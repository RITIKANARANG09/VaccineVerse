namespace Project
{
    public abstract class DataBase<T> where T: class
    {
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
            catch
            {
                return false;
            }
            return true;
        }
        
    }
}
