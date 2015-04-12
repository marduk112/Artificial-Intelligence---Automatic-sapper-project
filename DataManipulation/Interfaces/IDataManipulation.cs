using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManipulation.Structs;

namespace DataManipulation.Interfaces
{
    public interface IDataManipulation
    {
        Tuple<int, int, int> GetDisarmingProcedure(int beepsLevel);
    }
}
