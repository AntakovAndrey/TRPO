namespace TRPO.Models
{
    public class Role
    {
        public const string role = "e";
        public string getRole()
        {
            return role;
        }
    }
    public class AdminRole:Role
    {
        private const string role = "admin";
        public static string getRole()
        {
            return role;
        }
    }
    public class UserRole : Role 
    {
        private const string role = "admin";
        public static string getRole()
        {
            return role;
        }
    }

}
