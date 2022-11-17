using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPO.Interfaces
{
    public interface IRole
    {
        private static readonly string? _name;

        public string Name => _name;
    }
}