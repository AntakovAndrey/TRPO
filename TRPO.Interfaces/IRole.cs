using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPO.Interfaces
{
    public interface IRole
    {
        static string? Name { get; }
        List<IService>? AvaliableServices { get; }
    }
}
