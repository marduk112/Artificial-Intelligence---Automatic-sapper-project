using System;
using KnowledgeRepresentation;

namespace DataManipulation.Interfaces
{
    public interface IDataManipulation
    {
        Tuple<Disarming, Disarming, Disarming> GetDisarmingProcedure(int beepsLevel);
    }
}
