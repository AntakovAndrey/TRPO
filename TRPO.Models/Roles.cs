using TRPO.Interfaces;

namespace TRPO.Models
{
    public class AdminRole : IRole
    {   
        public static string Name
        {
            get  => "Admin";
        }
    }

    public class UserRole : IRole
    {
        public static string Name
        {
            get => "User";
        }
    }
}