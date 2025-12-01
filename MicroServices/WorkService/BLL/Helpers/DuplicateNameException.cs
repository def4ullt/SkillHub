using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public class DuplicateNameException : Exception
    {
        public DuplicateNameException(string entity, string name) : base($"{entity} with Name '{name}' already exists.")
        {

        }
    }
}
