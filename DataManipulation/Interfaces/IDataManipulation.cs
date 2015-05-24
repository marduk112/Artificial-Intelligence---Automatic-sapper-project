using System;
using System.Threading.Tasks;
using KnowledgeRepresentation;

namespace DataManipulation.Interfaces
{
    public interface IDataManipulation
    {
        Task<Tuple<Disarming, Disarming, Disarming>> GetDisarmingProcedure(int beepsLevel);
    }
}
