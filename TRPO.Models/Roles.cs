using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPO.Interfaces;

namespace TRPO.Models
{
    internal class AdminRole : IRole
    {
        string IRole.Name = "Admin";

        List<IService> IRole.AvaliableServices => throw new NotImplementedException();
    }
}
