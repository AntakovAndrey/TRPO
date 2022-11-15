using TRPO.Interfaces;

namespace TRPO.Models
{
    public class AdminRole : IRole
    {
        private static string _roleName = "Admin";
        
        List<IService> IRole.AvaliableServices => throw new NotImplementedException();

        static string Name
        {
            get => _roleName;
        }
    }

    public class UserRole : IRole
    {
        private static string _roleName = "User";

        List<IService> IRole.AvaliableServices => throw new NotImplementedException();

        static string Name
        {
            get => _roleName;
        }
    }
}