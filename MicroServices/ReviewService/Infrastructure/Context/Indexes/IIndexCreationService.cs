using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.Indexes
{
    public interface IIndexCreationService
    {
        void CreateIndexes();
    }
}
