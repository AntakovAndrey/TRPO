namespace TRPO.Models
{
    public class Role
    {
        public const string role="role";
        public string getRole()
        {
            return role;
        }
    }
    public class AdminRole:Role
    {
        private const string role = "admin";
        public string getRole()
        {
            return role;
        }
    }
    public class UserRole : Role 
    {
        private const string role = "admin";
        public string getRole()
        {
            return role;
        }
    }

}
