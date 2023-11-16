namespace Project
{
    public interface IDataBase<T> where T : class
    {
        bool AddItem(T obj, List<T> list, string path);
        bool UpdateItem(string path, List<T> list);
    }
}