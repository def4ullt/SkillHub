using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) 
        {
            
        }
    }
}
