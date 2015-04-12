using KnowledgeRepresentation.Bombs;
using KnowledgeRepresentation.Interfaces;

namespace KnowledgeRepresentation.Fabrics
{
    //Fabric which create bombs
    public static class BombFabric
    {
        public static IBomb CreateBomb(BombTypes bombType)
        {
            switch (bombType)
            {
                case BombTypes.Demolition:
                    return new DemolitionBomb();
                case BombTypes.Explosive:
                    return new ExplosiveBomb();
                case BombTypes.DemolitionExplosive:
                    return new DemolitionBomb();
                case BombTypes.Ball:
                    return new BallBomb();
                case BombTypes.Mine:
                    return new Mine();
                default:
                    return null;
            }
        }
    }
}
