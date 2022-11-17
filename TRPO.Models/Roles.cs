using TRPO.Interfaces;

namespace TRPO.Models
{
    public class AdminRole : IRole
    {
        private static string _roleName = "Admin";
        

        public static string Name
        {
            get  => _roleName;
        }
    }

    public class UserRole : IRole
    {
        private static string _roleName = "User";

        public string Name
        {
            get => _roleName;
        }

    }
}