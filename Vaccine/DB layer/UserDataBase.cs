
using Newtonsoft.Json;


namespace Project
{
    public class UserDataBase : DataBase<User>, IUserDataBase
    {

        private static UserDataBase _userInstance;
        public List<User> UsersList;
        private static string _userPath;
        public static UserDataBase UserInstance
        {
            get
            {
                if (_userInstance == null)
                {
                    _userInstance = new UserDataBase();
                }
                return _userInstance;
            }
        }

        private UserDataBase()
        {
            UsersList = new List<User>();
            _userPath = @"C:\Users\rnarang\OneDrive - WatchGuard Technologies Inc\Desktop\vjson.json";
            try
            {
                var allUsers = File.ReadAllText(_userPath);
                if (!String.IsNullOrEmpty(allUsers))
                    UsersList = JsonConvert.DeserializeObject<List<User>>(allUsers);
            }
            catch(Exception ex)
            {
                Errors.DbException();
                ExceptionController.LogException(ex, "Error occured while reading DB");
            }
        }
        public void AddUser(User newUser)
        {
            AddItem(newUser, UsersList, _userPath);
        }


    }
}
