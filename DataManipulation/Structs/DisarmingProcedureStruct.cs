using KnowledgeRepresentation;

namespace DataManipulation.Structs
{
    public class DisarmingProcedureStruct
    {
        public Disarming FirstStageDisarming { get; private set; }
        public Disarming SecondStageDisarming { get; private set; }
        public Disarming ThirdStageDisarming { get; private set; }

        public DisarmingProcedureStruct(Disarming firstStageDisarming, Disarming secondStageDisarming,
            Disarming thirdStageDisarming)
        {
            FirstStageDisarming = firstStageDisarming;
            SecondStageDisarming = secondStageDisarming;
            ThirdStageDisarming = thirdStageDisarming;
        }
    }
}