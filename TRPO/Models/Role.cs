namespace TRPO.Models
{
    public class Role
    {
        public const string role="role";
    }
    public class AdminRole:Role
    {
        public const string role = "admin";
    }
    public class UserRole : Role 
    {
        public const string role = "admin";
    }

}
