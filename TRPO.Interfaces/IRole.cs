using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPO.Interfaces
{
    public interface IRole
    {
        public static string Name { get; }
        public List<IService>? AvaliableServices { get; }
    }
}
