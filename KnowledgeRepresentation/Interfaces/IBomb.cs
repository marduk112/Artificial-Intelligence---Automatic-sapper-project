namespace KnowledgeRepresentation.Interfaces
{
    //parameters for bombs
    public interface IBomb
    {
        int BeepsLevel { get; }
        Disarming FirstStageDisarming { get; }
        Disarming SecondStageDisarming { get; }
        Disarming ThirdStageDisarming { get; }
    }
}
